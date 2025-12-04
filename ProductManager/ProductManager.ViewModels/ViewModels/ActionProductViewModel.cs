using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoryManager.Proxy.Interfaces;
using Domain.Categories;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using ProductManager.Proxy.Interfaces;
using ProductManager.ViewModels.Adapters;
using ProductManager.ViewModels.Models;

namespace ProductManager.ViewModels.ViewModels;

public class ActionProductViewModel
{
    private readonly IProductProxy _productProxy;
    private readonly ICategoryProxy _categoryProxy;
    private readonly ILogger<ActionProductViewModel> _logger;

    public ProductModel Product { get; set; } = new();

    public List<CategoryDto> Categories { get; private set; } = new();

    public event EventHandler<string>? OnFailure;

    public ActionProductViewModel(
        IProductProxy productProxy,
        ICategoryProxy categoryProxy,
        ILogger<ActionProductViewModel> logger)
    {
        _productProxy = productProxy;
        _categoryProxy = categoryProxy;
        _logger = logger;
    }

    public async Task InitializeViewModel()
    {
        Product = new ProductModel();
        await LoadCategoriesAsync();
    }

    public async Task EnsureCategoriesLoadedAsync()
    {
        if (Categories.Count == 0)
        {
            await LoadCategoriesAsync();
        }
    }

    private async Task LoadCategoriesAsync()
    {
        try
        {
            HandlerRequestResult<IEnumerable<CategoryDto>> result =
                await _categoryProxy.GetAllCategoriesAsync();

            if (!result.Success)
            {
                _logger.LogError("Failed to load categories: {ErrorMessage}", result.ErrorMessage);
                OnFailure?.Invoke(this, result.ErrorMessage);
                Categories = new();
                return;
            }

            var allCategories = result.SuccessValue ?? Enumerable.Empty<CategoryDto>();

            Categories = allCategories
                .Where(c => c.IsActive)
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error loading categories.");
            OnFailure?.Invoke(this, "Error cargando categorías.");
            Categories = new();
        }
    }

    public async Task<bool> CreateProductAsync()
    {
        var dto = Product.ToDto();
        HandlerRequestResult result = await _productProxy.AddProductAsync(dto);

        if (!result.Success)
        {
            _logger.LogError("Failed to save product:{ErrorMessage}", result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }

        return result.Success;
    }

    public async Task<bool> UpdateProductAsync()
    {
        var dto = Product.ToDto();
        HandlerRequestResult result = await _productProxy.UpdateProductAsync(dto);

        if (!result.Success)
        {
            _logger.LogError("Failed to update product {Id}: {ErrorMessage}", Product.Id, result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }

        return result.Success;
    }

    public async Task<bool> DesactivateProductAsync(int id)
    {
        HandlerRequestResult result = await _productProxy.DesactivateProductAsync(id);

        if (!result.Success)
        {
            _logger.LogError("Failed to delete product {Id}: {ErrorMessage}", id, result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }

        return result.Success;
    }

    public void ProductInEdit(ProductModel source)
    {
        Product = new ProductModel
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            CategoryId = source.CategoryId,
            CategoryName = source.CategoryName,
            IsActive = source.IsActive
        };
    }
}
