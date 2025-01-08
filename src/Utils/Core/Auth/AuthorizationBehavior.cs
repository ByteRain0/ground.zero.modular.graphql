using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using Core.Auth.Keycloack;
using Core.Otel;
using Core.Otel.Sources;
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
        using var evaluateAuthActivity = JapaneseApiRunTimeDiagnosticConfig.Source.StartActivity("Evaluate user entitlements");
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
            evaluateAuthActivity?.AddEvent(new ActivityEvent("No application and roles bundle was found."));
            var exception = new ForbiddenException("User is not authorized to this application.");
            evaluateAuthActivity?.AddExceptionAndFail(exception);
            throw exception;
        }

        if (CheckIfUserHasRequiredRoles(appUserRolesList, roles))
        {
            evaluateAuthActivity?.Stop();
            return await next();
        }

        evaluateAuthActivity?.SetStatus(ActivityStatusCode.Error);
        throw new ForbiddenException("User does not have the required claims.");
    }
    
    private AuthorizeRolesAttribute? GetAuthorizeRolesAttribute(TRequest request)
    {
        using var activity = JapaneseApiRunTimeDiagnosticConfig.Source?.StartActivity();
        return request!.GetType().GetCustomAttribute<AuthorizeRolesAttribute>();
    }
    
    private ClaimsIdentity GetAuthenticatedUserIdentity()
    {
        using var activity = JapaneseApiRunTimeDiagnosticConfig.Source?.StartActivity();
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
        {
            var exception = new ForbiddenException("User is not authenticated.");
            activity?.AddExceptionAndFail(exception);
            throw exception;
        }
        
        return identity;
    }

    private string GetResourceAccesses(ClaimsIdentity identity)
    {
        using var activity = JapaneseApiRunTimeDiagnosticConfig.Source?.StartActivity();
        var resourceAccesses = identity.Claims
            .FirstOrDefault(c => c.Type == AuthorizationConstants.IDPRolesClaimTypeName)?
            .Value;

        if (string.IsNullOrEmpty(resourceAccesses))
        {
            var exception = new ForbiddenException("User is not authorized to this application.");
            activity?.AddExceptionAndFail(exception);
            throw exception;
        }

        return resourceAccesses;
    }

    private Dictionary<string, ClientRoles>? ParseRetrievedUserRoles(string resourceAccesses)
    {
        using var activity = JapaneseApiRunTimeDiagnosticConfig.Source?.StartActivity();
        return JsonSerializer.Deserialize<Dictionary<string, ClientRoles>>(resourceAccesses);
    }

    private bool CheckIfUserHasRequiredRoles(Dictionary<string, ClientRoles> appUserRolesList, IEnumerable<string> requiredRoles)
    {
        using var activity = JapaneseApiRunTimeDiagnosticConfig.Source?.StartActivity();
        var appUserRoles = appUserRolesList
            .FirstOrDefault(x => x.Key == AuthorizationConstants.ApplicationName)
            .Value
            .Roles;
        
        Activity.Current?.SetTag("user_roles", appUserRoles.ToArray());
        return requiredRoles.Any(appUserRoles.Contains);
    }
}