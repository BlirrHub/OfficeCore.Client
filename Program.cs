using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OfficeCore.Client;
using System.Net.Http.Headers;
using Microsoft.JSInterop;
using OfficeCore.Client.Services.Api;
using OfficeCore.Client.Services.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["ApiBaseUrl"] ?? "http://10.33.20.154:6969/";
builder.Services.AddScoped(_ => 
    new HttpClient { BaseAddress = new Uri(apiUrl) });

builder.Services.AddSingleton<AuthState>();
builder.Services.AddScoped<AuthApi>();
builder.Services.AddScoped<AdminApi>();
builder.Services.AddScoped<PayrollApi>();
builder.Services.AddScoped<SettingsApi>();
builder.Services.AddScoped<LiquidationApi>();
builder.Services.AddScoped<LiquidationAdminApi>();
builder.Services.AddScoped<CashAdvanceApi>();
builder.Services.AddScoped<CashAdvanceAdminApi>();
builder.Services.AddScoped<PettyCashAdminApi>();

var host = builder.Build();

var js = host.Services.GetRequiredService<IJSRuntime>();
var auth = host.Services.GetRequiredService<AuthState>();
await auth.LoadAsync(js);

var http = host.Services.GetRequiredService<HttpClient>();
if (auth.IsAuthenticated && !string.IsNullOrWhiteSpace(auth.AccessToken))
{
    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.AccessToken);
}

await host.RunAsync();
