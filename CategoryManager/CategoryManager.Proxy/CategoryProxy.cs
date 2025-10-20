using Domain.Categories;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CategoryManager.Proxy;
public class CategoryProxy(HttpClient httpClient, ILogger<CategoryProxy> logger)
{
    private const string BaseRoute = "api/categories";


    public async Task<HandlerRequestResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync()
    {
        HandlerRequestResult<IEnumerable<CategoryDto>> result;
        try
        {
            var response = await httpClient.GetAsync(BaseRoute);
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult<IEnumerable<CategoryDto>>>();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching all categories.");
            throw;
        }
        return result;
    }

    public async Task<HandlerRequestResult> AddCategoryAsync(CategoryDto categoryDto)
    {
        HandlerRequestResult result;
        try
        {
            var response = await httpClient.PostAsJsonAsync(BaseRoute, categoryDto);
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult>();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error occurred while adding a new category.");
            throw;
        }
        return result;

    }

}
