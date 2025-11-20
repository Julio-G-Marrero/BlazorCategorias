using Microsoft.Extensions.DependencyInjection;
using ProductManager.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.ViewModels;

public static class DependencyContainer
{
    public static IServiceCollection AddProductManagerViewModels(this IServiceCollection services)
    {
        services.AddScoped<SearchProductViewModel>();
        services.AddScoped<ActionProductViewModel>();
        return services;
    }
}
