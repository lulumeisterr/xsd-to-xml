using br.com.dev.xsd.Application.Middleware;
using Microsoft.AspNetCore.Builder;

namespace br.com.dev.xsd.Application.Configuration
{
    public static class TokenValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenValidationMiddleware(this IApplicationBuilder builder, string accessToken)
        {
            return builder.UseMiddleware<TokenValidationMiddleware>(accessToken);
        }
    }
}
