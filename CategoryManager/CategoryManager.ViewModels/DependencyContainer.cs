using CategoryManager.ViewModels.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CategoryManager.ViewModels;
public static class DependencyContainer
{
    public static IServiceCollection AddCategoryManagerViewModels(this IServiceCollection services)
    {
        services.AddScoped<SearchCategoryViewModel>();
        return services;
    }

}
