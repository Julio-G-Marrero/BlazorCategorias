using System;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using UserManager.Proxy.Interfaces;
using UserManager.ViewModels.Adapters;
using UserManager.ViewModels.Models;

namespace UserManager.ViewModels.ViewModels;

public class ActionUserViewModel
{
    private readonly IUserProxy _userProxy;
    private readonly ILogger<ActionUserViewModel> _logger;
    public UserModel User { get; set; } = new();

    public event EventHandler<string>? OnFailure;

    public ActionUserViewModel(
        IUserProxy userProxy,
        ILogger<ActionUserViewModel> logger)
    {
        _userProxy = userProxy;
        _logger = logger;
    }

    public Task InitializeViewModel()
    {
        User = new UserModel();
        return Task.CompletedTask;
    }
    public async Task<bool> CreateUserAsync()
    {
        var dto = User.ToDto();
        HandlerRequestResult result = await _userProxy.CreateUserAsync(dto);

        if (!result.Success)
        {
            _logger.LogError("Failed to create user: {ErrorMessage}", result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }

        return result.Success;
    }

    public async Task<bool> UpdateUserAsync()
    {
        var dto = User.ToDto();
        HandlerRequestResult result = await _userProxy.UpdateUserAsync(dto);

        if (!result.Success)
        {
            _logger.LogError("Failed to update user {Id}: {ErrorMessage}", User.Id, result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }

        return result.Success;
    }

    public async Task<bool> DeactivateUserAsync(int id)
    {
        HandlerRequestResult result = await _userProxy.DeactivateUserAsync(id);

        if (!result.Success)
        {
            _logger.LogError("Failed to deactivate user {Id}: {ErrorMessage}", id, result.ErrorMessage);
            OnFailure?.Invoke(this, result.ErrorMessage);
        }

        return result.Success;
    }

    public void UserInEdit(UserModel source)
    {
        User = new UserModel
        {
            Id = source.Id,
            UserName = source.UserName,
            Surnames = source.Surnames,
            Email = source.Email,
            Password = source.Password,
            IsActive = source.IsActive,
        };
    }
}
