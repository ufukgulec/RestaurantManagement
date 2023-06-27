using RestaurantManagement.Application;
using RestaurantManagement.Application.Repositories;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Persistence.Contexts;

namespace RestaurantManagement.API.Middlewares
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration _configuration;


        public RequestResponseMiddleware(RequestDelegate Next, IConfiguration configuration)
        {
            next = Next;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context, ManagementContext managementContext)
        {
            try
            {
                await next.Invoke(context);
            }
            finally
            {
                if (Convert.ToBoolean(_configuration["RequestLogging"]))
                {
                    managementContext.Add(new Request()
                    {
                        Path = context.Request.Path,
                        Method = context.Request.Method,
                        StatusCode = context.Response.StatusCode.ToString(),
                        Active = true
                    });

                    await managementContext.SaveChangesAsync();
                }
            }
        }
    }
}