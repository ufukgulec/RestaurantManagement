namespace RestaurantManagement.API.Extensions
{
    public static class HealthCheckExtension
    {
        public static IApplicationBuilder UseHealthCheckExtension(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/api/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    await context.Response.WriteAsync("OK");
                }
            });
            return app;
        }
    }
}