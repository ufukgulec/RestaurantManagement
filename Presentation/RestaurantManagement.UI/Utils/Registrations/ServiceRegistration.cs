using RestaurantManagement.UI.Utils.Services;
using RestaurantManagement.UI.Utils.Services.Interfaces;

namespace RestaurantManagement.UI.Utils.Registrations
{
    public static class ServiceRegistration
    {
        public static void AddServiceRegistrations(this IServiceCollection collection)
        {
            collection.AddTransient<ICategoryService, CategoryService>();
            collection.AddTransient<INavigationMenuService, NavigationMenuService>();
        }
    }
}
