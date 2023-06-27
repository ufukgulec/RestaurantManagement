
using Microsoft.AspNetCore.Components.Authorization;


using RestaurantManagement.UI.Utils.Providers;
using RestaurantManagement.UI.Utils.Registrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("WepApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
});

builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();

    return clientFactory.CreateClient("WepApiClient");
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

#region Blazored
builder.Services.AddBlazoredRegistrations();
#endregion

#region Custom Managers
builder.Services.AddManagerRegistrations();
#endregion

#region Radzen Components
builder.Services.AddRadzenRegistrations();
#endregion

#region Services
builder.Services.AddServiceRegistrations();
#endregion

//builder.Services.AddAuthorizationCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();
