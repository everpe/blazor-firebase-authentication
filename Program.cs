using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazor;
using Blazor;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Blazor.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    provider => provider.GetRequiredService<ApiAuthenticationStateProvider>()
);

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var urlbaseApi = builder.Configuration.GetValue<string>("BaseUrl");
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(urlbaseApi!) });

await builder.Build().RunAsync();
