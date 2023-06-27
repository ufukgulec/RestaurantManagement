using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Radzen;
using RestaurantManagement.UI.Utils.Managers.Interfaces;
using System;

namespace RestaurantManagement.UI.Pages
{
    public class BasePage : ComponentBase
    {
        [Inject] public HttpClient Client { get; set; }
        [Inject] public INotificationManager NotificationManager { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ILocalStorageService localStorage { get; set; }
        [Inject] public DialogService DialogManager { get; set; }
        [Inject] public ContextMenuService ContextMenuManager { get; set; }
        [Inject] public TooltipService TooltipManager { get; set; }
    }
}
