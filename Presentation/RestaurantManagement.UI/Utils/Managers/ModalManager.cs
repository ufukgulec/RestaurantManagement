using Blazored.Modal;
using Blazored.Modal.Services;
using RestaurantManagement.UI.Utils.Components.Modals;
using RestaurantManagement.UI.Utils.Managers.Interfaces;

namespace RestaurantManagement.UI.Utils.Managers
{
    public class ModalManager : IModalManager
    {
        private readonly IModalService modalService;

        public ModalManager(IModalService modalService)
        {
            this.modalService = modalService;
        }

        /// <summary>
        /// Normal Modal Gösterme
        /// </summary>
        /// <param name="Title">Modalın Başlığı</param>
        /// <param name="Message">Modalın İçeriği</param>
        /// <param name="Color">Modalın Renklendirilmesi (Default=primary)</param>
        public async Task ShowModalAsync(string Title, string Message, string Color = "primary", int Duration = 0)
        {
            var options = new ModalOptions
            {
                UseCustomLayout = true
            };

            ModalParameters parameters = new ModalParameters();
            parameters.Add(nameof(DefaultModalComponent.Text), Message);
            parameters.Add(nameof(DefaultModalComponent.Caption), Title);
            parameters.Add(nameof(DefaultModalComponent.Color), string.IsNullOrEmpty(Color) ? "primary" : Color);
            var modalRes = modalService.Show<DefaultModalComponent>(Title, parameters, options);

            if (Duration > 0)
            {
                await Task.Delay(Duration);
                modalRes.Close();
            }
        }

        /// <summary>
        /// Alarm Modal Gösterme
        /// </summary>
        /// <param name="Caption">Alert Başlığı</param>
        /// <param name="Text">Alert İçeriği</param>
        /// <param name="OkText">Alert Devam Butonun İçeriği</param>
        /// <param name="Color">Alert Renklendirilmesi (Default=danger)</param>
        /// <param name="Icon">Alert İkon (Default=dripicons-information)</param>
        /// <returns></returns>
        public async Task ShowAlertAsync(string Caption, string Text, string OkText = "OK", string Color = "danger", string Icon = "dripicons-information", int Duration = 0)
        {
            var options = new ModalOptions
            {
                UseCustomLayout = true
            };

            ModalParameters parameters = new ModalParameters();
            parameters.Add(nameof(AlertModalComponent.Caption), Caption);
            parameters.Add(nameof(AlertModalComponent.Text), Text);
            parameters.Add(nameof(AlertModalComponent.Color), Color);
            parameters.Add(nameof(AlertModalComponent.OkText), OkText);
            parameters.Add(nameof(AlertModalComponent.Icon), Icon);
            var modalRes = modalService.Show<AlertModalComponent>(Text, parameters, options);

            if (Duration > 0)
            {
                await Task.Delay(Duration);
                modalRes.Close();
            }
        }

        public async Task<bool> ShowConfirmationAsync(string Caption, string Text, string YesText = "Yes", string NoText = "No", string Color = "info")
        {
            var options = new ModalOptions
            {
                UseCustomLayout = true
            };

            ModalParameters parameters = new ModalParameters();
            parameters.Add(nameof(ConfirmModalComponent.Text), Text);
            parameters.Add(nameof(ConfirmModalComponent.Caption), Caption);
            parameters.Add(nameof(ConfirmModalComponent.Color), Color);
            parameters.Add(nameof(ConfirmModalComponent.YesText), YesText);
            parameters.Add(nameof(ConfirmModalComponent.NoText), NoText);
            var modalRes = modalService.Show<ConfirmModalComponent>(Text, parameters, options);

            var result = await modalRes.Result;

            return !result.Cancelled;
        }
    }
}
