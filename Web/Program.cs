using CategoryManager.Proxy;
using CategoryManager.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web;
using Domain.Application.Abstractions;
using Web.Interop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<IToastService, ToastService>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddCategoryManagerViewModels();
builder.Services.AddCategoryManagerProxies(proxy =>
{
    proxy.BaseAddress = new Uri(builder.Configuration["WebApiAddress"]);
});
await builder.Build().RunAsync();
