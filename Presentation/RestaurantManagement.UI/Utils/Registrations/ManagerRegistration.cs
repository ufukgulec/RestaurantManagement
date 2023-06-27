using RestaurantManagement.UI.Utils.Managers;
using RestaurantManagement.UI.Utils.Managers.Interfaces;

namespace RestaurantManagement.UI.Utils.Registrations
{
    public static class ManagerRegistration
    {
        public static void AddManagerRegistrations(this IServiceCollection collection)
        {
            collection.AddTransient<IModalManager, ModalManager>();
            collection.AddTransient<INotificationManager, NotificationManager>();
        }
    }
}
