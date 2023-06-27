using RestaurantManagement.Domain.Entities;
using RestaurantManagement.UI.Utils.Extensions;
using RestaurantManagement.UI.Utils.Managers.Interfaces;
using RestaurantManagement.UI.Utils.Services.Interfaces;

namespace RestaurantManagement.UI.Utils.Services
{
    public class NavigationMenuService : BaseService, INavigationMenuService
    {

        public NavigationMenuService(HttpClient httpClient, INotificationManager notificationManager) : base(httpClient, notificationManager)
        {

        }

        public async Task<List<Navigation>> GetListAsync()
        {
            try
            {
                var ResponseResult = await Client.GetServiceResponseAsync<List<Navigation>>($"/api/Navigation/GetList");
                return ResponseResult;
            }
            catch (Exception e)
            {
                NotificationManager.ShowError(e.Message);
                return null;
            }
        }

        public async Task<bool> AddAsync(Navigation entity)
        {
            return true;
        }
        public async Task<bool> UpdateAsync(Navigation entity)
        {
            return true;
        }
        public async Task<bool> RemoveAsync(string id)
        {
            return true;
        }
    }
}
