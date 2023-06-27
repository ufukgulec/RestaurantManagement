using Radzen;

namespace RestaurantManagement.UI.Utils.Registrations
{
    public static class RadzenRegistration
    {
        public static void AddRadzenRegistrations(this IServiceCollection collection)
        {
            collection.AddScoped<DialogService>();
            collection.AddScoped<TooltipService>();
            collection.AddScoped<ContextMenuService>();
        }
    }
}
