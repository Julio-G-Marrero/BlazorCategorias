using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CategoryManager.Proxy.Interfaces;
using Domain.Categories;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace CategoryManager.Proxy;

public class CategoryProxy : ICategoryProxy
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CategoryProxy> _logger;

    private const string BaseRoute = "api/categories";

    public CategoryProxy(HttpClient httpClient, ILogger<CategoryProxy> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<HandlerRequestResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync()
    {
        HandlerRequestResult<IEnumerable<CategoryDto>> result;

        try
        {
            var response = await _httpClient.GetAsync(BaseRoute);
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult<IEnumerable<CategoryDto>>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all categories.");
            throw;
        }

        return result;
    }

    public async Task<HandlerRequestResult> AddCategoryAsync(CategoryDto categoryDto)
    {
        HandlerRequestResult result;

        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseRoute, categoryDto);
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a new category.");
            throw;
        }

        return result;
    }

    public async Task<HandlerRequestResult> UpdateCategoryAsync(CategoryDto categoryDto)
    {
        HandlerRequestResult result;

        try
        {
            var response = await _httpClient.PutAsJsonAsync(BaseRoute, categoryDto);
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating category {Id}.", categoryDto.Id);
            throw;
        }

        return result;
    }

    public async Task<HandlerRequestResult> DesactivateCategoryAsync(int id)
    {
        HandlerRequestResult result;

        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting category {Id}.", id);
            throw;
        }

        return result;
    }
}
