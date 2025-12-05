using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Users;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using UserManager.Proxy.Interfaces;
using UserManager.ViewModels.Adapters;
using UserManager.ViewModels.Models;

namespace UserManager.ViewModels.ViewModels;

public class SearchUserViewModel
{
    private readonly IUserProxy _proxy;
    private readonly ILogger<SearchUserViewModel> _logger;

    public List<UserModel> Users { get; set; } = new();

    public event EventHandler<string>? OnFailure;

    public SearchUserViewModel(IUserProxy proxy, ILogger<SearchUserViewModel> logger)
    {
        _proxy = proxy;
        _logger = logger;
    }

    public async Task InitializeViewModel()
    {
        await LoadUsersAsync();
    }

    public async Task LoadUsersAsync()
    {
        try
        {
            HandlerRequestResult<IEnumerable<UserDto>> result =
                await _proxy.GetAllUsersAsync();

            if (!result.Success)
            {
                _logger.LogError("Failed to load users: {ErrorMessage}", result.ErrorMessage);

                OnFailure?.Invoke(this,
                    result.ErrorMessage ?? "Error al cargar usuarios.");

                Users = new();
                return;
            }

            var data = result.SuccessValue ?? Array.Empty<UserDto>();
            Users = data.ToModelList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error loading users.");
            OnFailure?.Invoke(this, "Error inesperado al cargar usuarios.");
            Users = new();
        }
    }
}
