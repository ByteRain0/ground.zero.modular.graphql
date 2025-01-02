using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using Core.Auth.Keycloack;
using Core.Otel;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Auth;

public class AuthorizationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        using var evaluateAuthActivity = RunTimeDiagnosticConfig.Source.StartActivity("Evaluating user entitlements.");
        var attribute = GetAuthorizeRolesAttribute(request);
        if (attribute == null)
        {
            return await next();
        }
        
        var roles = attribute.Roles;
        evaluateAuthActivity?.SetTag("required_roles", roles);

        var identity = GetAuthenticatedUserIdentity();
        var resourceAccesses = GetResourceAccesses(identity);
        
        
        var appUserRolesList = ParseRetrievedUserRoles(resourceAccesses);
        if (appUserRolesList == null)
        {
            evaluateAuthActivity?.AddEvent(new ActivityEvent("No application and roles bundle was found"));
            throw new ForbiddenException("User is not authorized to this application.");
        }

        if (CheckIfUserHasRequiredRoles(appUserRolesList, roles))
        {
            evaluateAuthActivity?.Stop();
            return await next();
        }

        throw new ForbiddenException("User does not have the required claims.");
    }
    
    private AuthorizeRolesAttribute? GetAuthorizeRolesAttribute(TRequest request)
    {
        using var activity = RunTimeDiagnosticConfig.Source?.StartActivity();
        return request!.GetType().GetCustomAttribute<AuthorizeRolesAttribute>();
    }
    
    private ClaimsIdentity GetAuthenticatedUserIdentity()
    {
        using var activity = RunTimeDiagnosticConfig.Source?.StartActivity();
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
        {
            throw new ForbiddenException("User is not authenticated.");
        }
        return identity;
    }

    private string GetResourceAccesses(ClaimsIdentity identity)
    {
        using var activity = RunTimeDiagnosticConfig.Source?.StartActivity();
        var resourceAccesses = identity.Claims
            .FirstOrDefault(c => c.Type == AuthorizationConstants.IDPRolesClaimTypeName)?
            .Value;

        if (string.IsNullOrEmpty(resourceAccesses))
        {
            throw new ForbiddenException("User is not authorized to this application.");
        }

        return resourceAccesses;
    }

    private Dictionary<string, ClientRoles>? ParseRetrievedUserRoles(string resourceAccesses)
    {
        using var activity = RunTimeDiagnosticConfig.Source?.StartActivity();
        return JsonSerializer.Deserialize<Dictionary<string, ClientRoles>>(resourceAccesses);
    }

    private bool CheckIfUserHasRequiredRoles(Dictionary<string, ClientRoles> appUserRolesList, IEnumerable<string> requiredRoles)
    {
        using var activity = RunTimeDiagnosticConfig.Source?.StartActivity();
        var appUserRoles = appUserRolesList
            .FirstOrDefault(x => x.Key == AuthorizationConstants.ApplicationName)
            .Value
            .Roles;
        
        Activity.Current?.SetTag("user_roles", appUserRoles.ToArray());
        return requiredRoles.Any(appUserRoles.Contains);
    }
}