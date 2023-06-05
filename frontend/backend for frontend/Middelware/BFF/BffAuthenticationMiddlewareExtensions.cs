namespace backend_for_frontend.Middelware.BFF
{
    public static class BffAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseBffAuthentication(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BffAuthenticationMiddleware>();
        }
    }
}
