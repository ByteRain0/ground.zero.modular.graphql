using System.Net.Http.Headers;

namespace Gateway;

public sealed class AuthenticationMiddleware(IHttpContextAccessor accessor) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (accessor.HttpContext is not null && accessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var value))
        {
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(value.ToString());
        }
        
        return base.SendAsync(request, cancellationToken);
    }
}