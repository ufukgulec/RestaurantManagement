namespace RestaurantManagement.UI.Utils.Managers.Interfaces
{
    public interface INotificationManager
    {
        void ShowError(string Text);
        void ShowInfo(string Text);
        void ShowSuccess(string Text);
        void ShowWarning(string Text);
    }
}