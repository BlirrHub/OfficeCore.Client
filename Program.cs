using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OfficeCore.Client;
using OfficeCore.Client.Services.Api;
using OfficeCore.Client.Services.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:6969/";
builder.Services.AddScoped(_ => 
    new HttpClient { BaseAddress = new Uri(apiUrl) });

builder.Services.AddSingleton<AuthState>();
builder.Services.AddScoped<AuthApi>();
builder.Services.AddScoped<AdminApi>();
builder.Services.AddScoped<PayrollApi>();

await builder.Build().RunAsync();
