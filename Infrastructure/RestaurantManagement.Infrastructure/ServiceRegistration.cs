using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.GPT3.Extensions;
using RestaurantManagement.Application;

namespace RestaurantManagement.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServicesAsync(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenAIService(settings
                => settings.ApiKey = "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");


            services.AddTransient<IServiceOfWork, ServiceOfWork>();
        }

    }
}
