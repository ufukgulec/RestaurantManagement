using RestaurantManagement.UI.Utils.Managers.Interfaces;
using RestaurantManagement.UI.Utils.Services.Interfaces;

namespace RestaurantManagement.UI.Utils.Services
{
    public class BaseService : IBaseService
    {
        public HttpClient Client;
        public INotificationManager NotificationManager;

        public BaseService(HttpClient httpClient, INotificationManager notificationManager)
        {
            Client = httpClient;
            this.NotificationManager = notificationManager;
        }
    }
}
