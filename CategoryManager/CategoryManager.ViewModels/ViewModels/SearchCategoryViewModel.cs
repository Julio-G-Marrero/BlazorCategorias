using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoryManager.Proxy.Interfaces;
using CategoryManager.ViewModels.Adapters;
using CategoryManager.ViewModels.Models;
using Domain.Categories;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace CategoryManager.ViewModels.ViewModels;

public class SearchCategoryViewModel(ICategoryProxy proxy, ILogger<SearchCategoryViewModel> logger)
{
    public List<CategoryModel> Categories { get; set; } = new();

    public EventHandler<string>? OnFailure { get; set; }

    public async Task InitializeViewModel()
    {
        await LoadCategoriesAsync();
    }

    public async Task LoadCategoriesAsync()
    {
        HandlerRequestResult<IEnumerable<CategoryDto>> result =
            await proxy.GetAllCategoriesAsync();

        if (result.Success)
        {
            var data = result.SuccessValue ?? Enumerable.Empty<CategoryDto>();

            Categories = data.ToModelList();
        }
        else
        {
            logger.LogError("Failed to load categories: {ErrorMessage}", result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }
    }
}
