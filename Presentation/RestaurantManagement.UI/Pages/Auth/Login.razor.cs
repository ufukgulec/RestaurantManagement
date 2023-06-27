using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RestaurantManagement.Shared.DTO;
using RestaurantManagement.UI.Utils.Extensions;
using RestaurantManagement.UI.Utils.Providers;

namespace RestaurantManagement.UI.Pages.Auth
{

    public partial class Login : BasePage
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private async Task LoginAsync()
        {
            //try
            //{
            //    var response = await Client.GetServiceResponseAsync<LoginUserInfoDTO>($"api/Auth/Login/5075452953", true);

            //    if (response.Success)
            //    {
            //        await localStorage.SetItemAsync("token", response.Result.AccessToken);
            //        await localStorage.SetItemAsync("userId", response.Result.Employee.Id);
            //        await localStorage.SetItemAsync("userFullname", response.Result.Employee.Fullname);
            //        await localStorage.SetItemAsync("userRole", response.Result.Employee.Role.Name);
            //        await localStorage.SetItemAsync("userPhoneNumber", response.Result.Employee.PhoneNumber);
            //        NotificationManager.ShowSuccess("Başarılı giriş");

            //        (AuthenticationStateProvider as AuthStateProvider).NotifyUserLogin(response.Result.Employee.Id.ToString());

            //        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.Result.AccessToken);

            //        NavigationManager.NavigateTo("/");
            //    }
            //}
            //catch (Exception exc)
            //{
            //    NotificationManager.ShowError(exc.Message);
            //}
        }
    }
}
