using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RestaurantManagement.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServicesAsync(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();

            //services.AddAutoMapper(assm);
            //services.AddValidatorsFromAssembly(assm);

            return services;
        }

    }
}
