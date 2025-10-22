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

    public async Task<bool> UpdateCategoryAsync()
    {
        HandlerRequestResult result = await proxy.UpdateCategoryAsync(Category.ToDto());
        if (!result.Success)
        {
            logger.LogError("Failed to update category {Id}: {ErrorMessage}", Category.Id, result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }
        return result.Success;
    }

    public async Task<bool> DesactivateCategoryAsync(int id)
    {
        HandlerRequestResult result = await proxy.DesactivateCategoryAsync(id);
        if (!result.Success)
        {
            logger.LogError("Failed to delete category {Id}: {ErrorMessage}", id, result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }
        return result.Success;
    }

    public void CategoryInEdit(CategoryModel source)
    {
        Category = new CategoryModel
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            IsActive = source.IsActive
        };
    }

}
