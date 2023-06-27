using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace RestaurantManagement.UI.Utils.Providers
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationState anonymous;

        public AuthStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
            anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await localStorageService.GetItemAsStringAsync("token");
            if (string.IsNullOrEmpty(token))
                return anonymous;

            IdentityOptions _options = new IdentityOptions();

            string userId = await localStorageService.GetItemAsStringAsync("userId");
            string userFullname = await localStorageService.GetItemAsStringAsync("userFullname");
            string userRole = await localStorageService.GetItemAsStringAsync("userRole");

            var cp = new ClaimsPrincipal(new ClaimsIdentity(
                new[] {
                    new Claim(_options.ClaimsIdentity.UserIdClaimType, userId),
                    new Claim(_options.ClaimsIdentity.UserNameClaimType, userFullname),
                    new Claim(_options.ClaimsIdentity.RoleClaimType, userRole),
                }, "jwtAuthType"));

            return new AuthenticationState(cp);
        }

        public void NotifyUserLogin(string userId)
        {
            IdentityOptions _options = new IdentityOptions();

            var cp = new ClaimsPrincipal(new ClaimsIdentity(
                new[] {
                    new Claim(_options.ClaimsIdentity.UserIdClaimType, userId)
                }, "jwtAuthType"));

            var authState = Task.FromResult(new AuthenticationState(cp));

            NotifyAuthenticationStateChanged(authState);
        }
        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(anonymous);

            NotifyAuthenticationStateChanged(authState);

        }
    }
}
