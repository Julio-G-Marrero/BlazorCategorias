using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Proxy;
public static class DependencyContainer
{
    public static IServiceCollection AddProductManagerProxies(this IServiceCollection services, Action<HttpClient> configureProxy)
    {
        services.AddHttpClient<ProductProxy>(configureProxy);

        return services;
    }
}
