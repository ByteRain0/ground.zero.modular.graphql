using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using Core.Auth.Keycloack;
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
        var attribute = request!.GetType().GetCustomAttribute<AuthorizeRolesAttribute>();
        
        if (attribute == null)
        {
            return await next();
        }
        
        var roles = attribute.Roles;
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.User?.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
        {
            throw new ForbiddenException("User is not authenticated.");
        }
        
        var resourceAccesses = identity.Claims
            .FirstOrDefault(c => c.Type == AuthorizationConstants.RolesClaimTypeName)?
            .Value;

        if (string.IsNullOrEmpty(resourceAccesses))
        {
            throw new ForbiddenException("User is not authorized to this applicaion.");
        }
        
        var appUserRoles = JsonSerializer.Deserialize<Dictionary<string, ClientRoles>>(resourceAccesses)!
            .FirstOrDefault(x => x.Key == AuthorizationConstants.ApplicationName)
            .Value
            .Roles;
        
        if (!roles.Any(appUserRoles.Contains))
        {
            throw new ForbiddenException("User does not have the required claims.");
        }
        
        return await next();
    }
}