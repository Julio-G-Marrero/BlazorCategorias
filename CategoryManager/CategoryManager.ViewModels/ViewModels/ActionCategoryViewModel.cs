using CategoryManager.Proxy;
using CategoryManager.ViewModels.Adapters;
using CategoryManager.ViewModels.Models;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace CategoryManager.ViewModels.ViewModels;

public class ActionCategoryViewModel(CategoryProxy proxy, ILogger<ActionCategoryViewModel> logger)
{
    public CategoryModel Category { get; set; }

    public event EventHandler<string>? OnFailure;

    public Task InitializeViewModel()
    {
        Category = new();
        return Task.CompletedTask;
    }

    public async Task<bool> CreateCategoryAsync()
    {
        HandlerRequestResult result = await proxy.AddCategoryAsync(Category.ToDto());
        if (!result.Success)
        {
            logger.LogError("Failed to save category: {ErrorMessage}", result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }
        return result.Success;
    }
}
