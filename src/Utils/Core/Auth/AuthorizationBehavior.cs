using System.Reflection;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Auth;

/// <summary>
/// MediatR based auth pipeline behavior.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
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

        var userRoles = identity.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value);

        if (!roles.Any(userRoles.Contains))
        {
            throw new ForbiddenException("User does not have the required claims.");
        }

        return await next();
    }
}