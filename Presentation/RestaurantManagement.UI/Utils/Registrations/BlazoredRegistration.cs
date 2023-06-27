using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;

namespace RestaurantManagement.UI.Utils.Registrations
{
    public static class BlazoredRegistration
    {
        public static void AddBlazoredRegistrations(this IServiceCollection collection)
        {
            collection.AddBlazoredModal();
            collection.AddBlazoredToast();
            collection.AddBlazoredLocalStorage();
        }
    }
}
