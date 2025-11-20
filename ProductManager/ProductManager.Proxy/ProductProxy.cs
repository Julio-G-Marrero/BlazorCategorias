using Domain.Categories;
using Domain.Products;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Proxy;
public class ProductProxy(HttpClient httpClient, ILogger<ProductProxy> logger)
{
    private const string BaseRoute = "api/products";

    public async Task<HandlerRequestResult<IEnumerable<ProductDto>>> GetAllProductAsync()
    {
        HandlerRequestResult<IEnumerable<ProductDto>> result;
        try
        {
            var response = await httpClient.GetAsync(BaseRoute);
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult<IEnumerable<ProductDto>>>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching all products.");
            throw;
        }
        return result;
    }

    public async Task<HandlerRequestResult> AddProductAsync(ProductDto productDto)
    {
        HandlerRequestResult result;
        try
        {
            var response = await httpClient.PostAsJsonAsync(BaseRoute, productDto);
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while adding a new product.");
            throw;
        }
        return result;

    }
    public async Task<HandlerRequestResult> UpdateProductAsync(ProductDto productDto)
    {
        HandlerRequestResult result;
        try
        {
            var response = await httpClient.PutAsJsonAsync(BaseRoute, productDto);
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating product {Id}.", productDto.Id);
            throw;
        }
        return result;
    }
    public async Task<HandlerRequestResult> DesactivateProductAsync(int id)
    {
        HandlerRequestResult result;
        try
        {
            var url = $"{BaseRoute}/{id}";
            var response = await httpClient.DeleteAsync(url);

            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting product {Id}.", id);
            throw;
        }
        return result;
    }
}
