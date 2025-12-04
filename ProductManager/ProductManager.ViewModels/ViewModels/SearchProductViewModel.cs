using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Products;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using ProductManager.Proxy.Interfaces;
using ProductManager.ViewModels.Adapters;
using ProductManager.ViewModels.Models;

namespace ProductManager.ViewModels.ViewModels;

public class SearchProductViewModel(IProductProxy proxy, ILogger<SearchProductViewModel> logger)
{
    public List<ProductModel> Products { get; set; } = new();

    public EventHandler<string>? OnFailure { get; set; }

    public async Task InitializeViewModel()
    {
        await LoadProductsAsync();
    }

    public async Task LoadProductsAsync()
    {
        HandlerRequestResult<IEnumerable<ProductDto>> result =
            await proxy.GetAllProductAsync();

        if (result.Success)
        {
            var data = result.SuccessValue ?? Array.Empty<ProductDto>();
            Products = data.ToModelList();
        }
        else
        {
            logger.LogError("Failed to load products: {ErrorMessage}", result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }
    }
}
