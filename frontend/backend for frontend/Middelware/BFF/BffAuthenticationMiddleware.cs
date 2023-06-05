using Microsoft.AspNetCore.Authentication;

namespace backend_for_frontend.Middelware.BFF
{
    public class BffAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public BffAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!IsUserAuthenticated(context))
            {
                if (!IsAjaxRequest(context.Request))
                {
                    // Redirect to login and stop pipeline
                    await context.ChallengeAsync();
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
                return;
            }

            await _next(context);
        }

        private static bool IsUserAuthenticated(HttpContext context)
        {
            return context.User.Identity?.IsAuthenticated ?? false;
        }

        private static bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers != null &&request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
