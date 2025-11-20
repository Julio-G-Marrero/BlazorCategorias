using Domain.Categories;
using Domain.Products;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using ProductManager.Proxy;
using ProductManager.ViewModels.Adapters;
using ProductManager.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.ViewModels.ViewModels;

public class SearchProductViewModel(ProductProxy proxy, ILogger<SearchProductViewModel> logger)
{
    public List<ProductModel> Products { get; set; }
    public EventHandler<string> OnFailure { get; set; }
    public async Task InitializeViewModel()
    {
        await LoadProductsAsync();
    }
    public async Task LoadProductsAsync()
    {
        HandlerRequestResult<IEnumerable<ProductDto>> result;
        result = await proxy.GetAllProductAsync();
        if (result.Success)
        {
            Products = result.SuccessValue.ToModelList();
        }
        else
        {
            logger.LogError("Failed to load products: {ErrorMessage}", result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }
    }
}
