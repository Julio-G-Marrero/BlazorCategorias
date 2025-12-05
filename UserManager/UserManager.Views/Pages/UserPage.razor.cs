using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using UserManager.ViewModels.Models;
using UserManager.ViewModels.ViewModels;

namespace UserManager.Views.Pages;

public partial class UserPage : ComponentBase, IDisposable
{
    [Inject] public SearchUserViewModel ViewModel { get; set; } = default!;
    [Inject] public ActionUserViewModel ActionUserViewModel { get; set; } = default!;
    [Inject] public ILogger<UserPage> Logger { get; set; } = default!;

    private bool IsLoading;
    private bool ShowDesactivateConfirm;
    private int PendingDesactivateId;
    private bool ShowCreateModal;
    private bool ShowEditModal;

    private string? ErrorMessage;
    private bool ShowErrorAlert;

    protected override async Task OnInitializedAsync()
    {
        ViewModel.OnFailure += HandleFailure;
        await ViewModel.InitializeViewModel();
    }

    private async Task OpenCreateModal()
    {
        await ActionUserViewModel.InitializeViewModel();
        ShowCreateModal = true;
        StateHasChanged();
    }

    private void CloseCreateModal()
    {
        ShowCreateModal = false;
    }

    private async Task CreateAsync()
    {
        try
        {
            IsLoading = true;
            var result = await ActionUserViewModel.CreateUserAsync();
            if (result)
            {
                ShowCreateModal = false;
                await ViewModel.InitializeViewModel();
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al crear usuario.");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void OpenEditModal(UserModel user)
    {
        ActionUserViewModel.UserInEdit(user);
        ShowEditModal = true;
    }

    private void CloseEditModal()
    {
        ShowEditModal = false;
    }

    private async Task UpdateAsync()
    {
        try
        {
            IsLoading = true;
            var result = await ActionUserViewModel.UpdateUserAsync();
            if (result)
            {
                ShowEditModal = false;
                await ViewModel.InitializeViewModel();
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al actualizar usuario.");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void AskDesactivate(int userId)
    {
        PendingDesactivateId = userId;
        ShowDesactivateConfirm = true;
    }

    private void CancelDesactivate()
    {
        ShowDesactivateConfirm = false;
        PendingDesactivateId = 0;
    }

    private async Task ConfirmDesactivateAsync()
    {
        if (PendingDesactivateId == 0)
            return;

        IsLoading = true;
        try
        {
            var result = await ActionUserViewModel.DeactivateUserAsync(PendingDesactivateId);
            if (result)
            {
                ShowDesactivateConfirm = false;
                PendingDesactivateId = 0;
                await ViewModel.InitializeViewModel();
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al desactivar usuario");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void HandleFailure(object? sender, string errorMessage)
    {
        Logger.LogWarning("Fallo VM: {msg}", errorMessage);
        ErrorMessage = errorMessage;
        ShowErrorAlert = true;
        InvokeAsync(StateHasChanged);
    }
    private void CloseErrorAlert()
    {
        ShowErrorAlert = false;
        ErrorMessage = null;
    }

    public void Dispose()
    {
        ViewModel.OnFailure -= HandleFailure;
    }
}
