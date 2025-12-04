using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.ViewModels.ViewModels;

namespace UserManager.ViewModels;

public static class DependencyContainer
{
    public static IServiceCollection AddUserManagerViewModels(this IServiceCollection services)
    {
        services.AddScoped<SearchUserViewModel>();
        services.AddScoped<ActionUserViewModel>();
        return services;
    }
}
