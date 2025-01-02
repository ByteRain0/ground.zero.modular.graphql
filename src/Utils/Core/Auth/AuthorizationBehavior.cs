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
        using var evaluateAuthActivity = 
            RunTimeDiagnosticConfig.Source.StartActivity("Evaluating user entitlements.");
        
        var attribute = request!.GetType().GetCustomAttribute<AuthorizeRolesAttribute>();
        
        if (attribute == null)
        {
            return await next();
        }
        
        var roles = attribute.Roles;
        
        evaluateAuthActivity?.SetTag("required_roles", roles);
        
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User?.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
        {
            throw new ForbiddenException("User is not authenticated.");
        }
        
        var resourceAccesses = identity.Claims
            .FirstOrDefault(c => c.Type == AuthorizationConstants.IDPRolesClaimTypeName)?
            .Value;

        if (string.IsNullOrEmpty(resourceAccesses))
        {
            throw new ForbiddenException("User is not authorized to this application.");
        }

        var appUserRolesList = JsonSerializer.Deserialize<Dictionary<string, ClientRoles>>(resourceAccesses);

        if (appUserRolesList is null)
        {
            evaluateAuthActivity?.AddEvent(new ActivityEvent("No application and roles bundle was found"));
        }
        else
        {
            var appUserRoles = appUserRolesList
                .FirstOrDefault(x => x.Key == AuthorizationConstants.ApplicationName)
                .Value
                .Roles;
        
            if (roles.Any(appUserRoles.Contains))
            {
                return await next();
            }
        }
        
        throw new ForbiddenException("User does not have the required claims.");
    }
}