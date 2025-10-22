using CategoryManager.Proxy;
using CategoryManager.ViewModels.Adapters;
using CategoryManager.ViewModels.Models;
using Domain.Categories;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace CategoryManager.ViewModels.ViewModels;
public class SearchCategoryViewModel(CategoryProxy proxy, ILogger<SearchCategoryViewModel> logger)
{
    public List<CategoryModel> Categories { get; set; }

    public EventHandler<string> OnFailure { get; set; }

    public async Task InitializeViewModel()
    {
        await LoadCategoriesAsync();
    }


    public async Task LoadCategoriesAsync()
    {
        HandlerRequestResult<IEnumerable<CategoryDto>> result;
        result = await proxy.GetAllCategoriesAsync();
        if (result.Success)
        {
            Categories = result.SuccessValue.ToModelList();
        }
        else
        {
            logger.LogError("Failed to load categories: {ErrorMessage}", result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }
    }
}
