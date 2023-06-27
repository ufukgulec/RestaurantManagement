using Blazored.Toast;
using Blazored.Toast.Configuration;
using Blazored.Toast.Services;
using RestaurantManagement.UI.Utils.Managers.Interfaces;

namespace RestaurantManagement.UI.Utils.Managers
{
    public class NotificationManager : INotificationManager
    {
        private readonly IToastService toastService;

        public NotificationManager(IToastService toastService)
        {
            this.toastService = toastService;
        }

        public void ShowInfo(string Text)
        {
            toastService.ShowInfo(Text);
        }
        public void ShowError(string Text)
        {
            toastService.ShowError(Text);
        }
        public void ShowSuccess(string Text)
        {
            toastService.ShowSuccess(Text);
        }
        public void ShowWarning(string Text)
        {
            toastService.ShowWarning(Text);
        }
    }
}
