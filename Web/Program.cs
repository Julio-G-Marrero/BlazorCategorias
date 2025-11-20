using CategoryManager.Proxy;
using ProductManager.Proxy;
using CategoryManager.ViewModels;
using ProductManager.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddCategoryManagerViewModels();
builder.Services.AddCategoryManagerProxies(proxy =>
{
    proxy.BaseAddress = new Uri(builder.Configuration["WebApiAddress"]);
});
builder.Services.AddProductManagerProxies(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WebApiAddress"]);
});
builder.Services.AddProductManagerViewModels();
await builder.Build().RunAsync();
