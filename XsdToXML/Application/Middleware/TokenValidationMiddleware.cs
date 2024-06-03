using Microsoft.AspNetCore.Http;
namespace br.com.dev.xsd.Application.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _accessToken;

        public TokenValidationMiddleware(RequestDelegate next, string accessToken)
        {
            _next = next;
            _accessToken = accessToken;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string accessToken = context.Request.Headers["Authorization"];
            if (accessToken != $"Bearer {_accessToken}")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            await _next(context);
        }
    }
}
