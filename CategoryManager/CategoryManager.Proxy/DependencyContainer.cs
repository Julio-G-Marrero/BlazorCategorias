
using Microsoft.Extensions.DependencyInjection;

namespace CategoryManager.Proxy;
public static class DependencyContainer
{
    public static IServiceCollection AddCategoryManagerProxies(this IServiceCollection services, Action<HttpClient> configureProxy)
    {
        services.AddHttpClient<CategoryProxy>(configureProxy);

        return services;
    } 
}
