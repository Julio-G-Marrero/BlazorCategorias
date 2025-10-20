using BlazorCategorias.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;

namespace BlazorCategorias;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        var baseUrl = builder.Configuration["Api:BaseUrl"]
            ?? throw new InvalidOperationException("Falta Api:BaseUrl en appsettings.json");

        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        });

        builder.Services.AddScoped<ICategoriesCliente, CategoriesCliente>();

        await builder.Build().RunAsync();
    }
}
