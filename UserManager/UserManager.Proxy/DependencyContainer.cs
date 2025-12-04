using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.Proxy;

public static class DependencyContainer
{
    public static IServiceCollection AddUserManagerProxies(this IServiceCollection services, Action<HttpClient> configureProxy)
    {
        services.AddHttpClient<UserProxy>(configureProxy);
        return services;
    }
}
