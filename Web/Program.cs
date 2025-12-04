using CategoryManager.Proxy;
using ProductManager.Proxy;
using UserManager.Proxy;

using CategoryManager.ViewModels;
using ProductManager.ViewModels;
using UserManager.ViewModels;

using CategoryManager.Proxy.Interfaces;
using ProductManager.Proxy.Interfaces;
using UserManager.Proxy.Interfaces;

using ProductManager.ViewModels.ViewModels;
using UserManager.ViewModels.ViewModels;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddCategoryManagerViewModels();

builder.Services.AddProductManagerViewModels();

// Usuarios (si tienes un AddUserManagerViewModels, úsalo)
builder.Services.AddUserManagerViewModels();
// Si todavía NO tienes extensión, puedes reemplazar la línea anterior por:
// builder.Services.AddScoped<SearchUserViewModel>();
// builder.Services.AddScoped<ActionUserViewModel>();

builder.Services.AddHttpClient<ICategoryProxy, CategoryProxy>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WebApiAddress"]);
});

builder.Services.AddHttpClient<IProductProxy, ProductProxy>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WebApiAddress"]);
});

builder.Services.AddHttpClient<IUserProxy, UserProxy>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WebApiAddress"]);
});

builder.Services.AddScoped<CategoryProxy>(sp =>
    (CategoryProxy)sp.GetRequiredService<ICategoryProxy>());

builder.Services.AddScoped<ProductProxy>(sp =>
    (ProductProxy)sp.GetRequiredService<IProductProxy>());

builder.Services.AddScoped<UserProxy>(sp =>
    (UserProxy)sp.GetRequiredService<IUserProxy>());

builder.Services.AddScoped<ActionProductViewModel>();
builder.Services.AddScoped<ActionUserViewModel>();

await builder.Build().RunAsync();
