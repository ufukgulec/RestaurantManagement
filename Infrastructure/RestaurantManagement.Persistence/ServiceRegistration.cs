using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Application;
using RestaurantManagement.Persistence.Contexts;
using RestaurantManagement.Persistence.SeedDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServicesAsync(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration["ConnectionString"].ToString();
            services.AddDbContext<ManagementContext>(options => options.UseSqlServer(conn));

            //services.AddScoped<ICountryRepository, CountryRepository>();
            //services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //var seedData = new SeedData();
            //await seedData.SeedAsync(configuration);
        }

    }
}
