using Domain.Users;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.Proxy.Interfaces;
using UserManager.ViewModels.Adapters;
using UserManager.ViewModels.Models;

namespace UserManager.ViewModels.ViewModels;

public class SearchUserViewModel(IUserProxy proxy, ILogger<SearchUserViewModel> logger)
{
    public List<UserModel> Users { get; set; } = new();
    public EventHandler<string>? OnFailure { get; set; }

    public async Task InitializeViewModel()
    {
        await LoadUsersAsync();
    }
    public async Task LoadUsersAsync()
    {
        HandlerRequestResult<IEnumerable<UserDto>> result =
            await proxy.GetAllUsersAsync();

        if(result.Success)
        {
            var data = result.SuccessValue ?? Array.Empty<UserDto>();
            Users = data.ToModelList();
        }
        else
        {
            logger.LogError("Failed to load users {ErrorMessage}", result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }
    }
}
