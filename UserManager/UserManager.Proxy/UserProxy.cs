using Domain.Users;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UserManager.Proxy.Interfaces;

namespace UserManager.Proxy;

public class UserProxy : IUserProxy
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserProxy> _logger;

    private const string BaseRoute = "api/users";

    public UserProxy(HttpClient httpClient, ILogger<UserProxy> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<HandlerRequestResult<IEnumerable<UserDto>>> GetAllUsersAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(BaseRoute);

            var result = await response.Content
                .ReadFromJsonAsync<HandlerRequestResult<IEnumerable<UserDto>>>();

            if (result is null)
            {
                return new HandlerRequestResult<IEnumerable<UserDto>>(
                    "No se pudo obtener respuesta válida del servidor.");
            }

            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all users (HTTP error).");

            return new HandlerRequestResult<IEnumerable<UserDto>>(
                "No se pudo conectar con el servidor.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while fetching all users.");

            return new HandlerRequestResult<IEnumerable<UserDto>>(
                "Ocurrió un error inesperado al cargar los usuarios.");
        }
    }

    public async Task<HandlerRequestResult> CreateUserAsync(UserDto dto)
    {
        HandlerRequestResult result;

        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseRoute, dto);
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new user.");
            throw;
        }

        return result;
    }

    public async Task<HandlerRequestResult> UpdateUserAsync(UserDto dto)
    {
        HandlerRequestResult result;

        try
        {
            var response = await _httpClient.PutAsJsonAsync(BaseRoute, dto);
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating user {Id}.", dto.Id);
            throw;
        }

        return result;
    }

    public async Task<HandlerRequestResult> DeactivateUserAsync(int id)
    {
        HandlerRequestResult result;

        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
            result = await response.Content.ReadFromJsonAsync<HandlerRequestResult>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deactivating user {Id}.", id);
            throw;
        }

        return result;
    }
}
