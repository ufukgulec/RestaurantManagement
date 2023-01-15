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
