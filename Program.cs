using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OfficeCore.Client;
using OfficeCore.Client.Services.Api;
using OfficeCore.Client.Services.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<AuthState>();
builder.Services.AddScoped<AuthApi>();
builder.Services.AddScoped<AdminApi>();

builder.Services.AddScoped(_ => 
    new HttpClient { BaseAddress = new Uri("http://localhost:5205/") });

await builder.Build().RunAsync();
