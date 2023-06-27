using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Persistence.Contexts;
using RestaurantManagement.Shared.CustomExceptions;
using System.Net;

namespace RestaurantManagement.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate Next)
        {
            next = Next;
        }
        public async Task Invoke(HttpContext context, ManagementContext managementContext)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                managementContext.Add(new Log()
                {
                    Active = true,
                    Message = e.Message,
                    LogType = LogType.Error,
                });

                await managementContext.SaveChangesAsync();

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (e is AuthException)
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                if (e is ApiException)
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;


                await context.Response.WriteAsync(e.Message);
            }
        }
    }
}
