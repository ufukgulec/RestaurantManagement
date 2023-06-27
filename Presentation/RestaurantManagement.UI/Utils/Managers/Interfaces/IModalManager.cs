namespace RestaurantManagement.UI.Utils.Managers.Interfaces
{
    public interface IModalManager
    {
        Task ShowAlertAsync(string Caption, string Text, string OkText = "OK", string Color = "danger", string Icon = "dripicons-information", int Duration = 0);
        Task<bool> ShowConfirmationAsync(string Caption, string Text, string YesText = "Yes", string NoText = "No", string Color = "info");
        Task ShowModalAsync(string Title, string Message, string Color = "primary", int Duration = 0);
    }
}