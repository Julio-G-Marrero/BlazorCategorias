using System;
using System.Threading.Tasks;
using Domain.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ProductManager.ViewModels.Models;
using ProductManager.ViewModels.ViewModels;
// using Common.Views.Toastify;
// using Common.Views.Animations;

namespace ProductManager.Views.Pages;

public partial class ProductPage : ComponentBase, IDisposable
{
    [Inject]
    public SearchProductViewModel ViewModel { get; set; } = default!;

    [Inject]
    public ActionProductViewModel ActionProductViewModel { get; set; } = default!;

    [Inject]
    public ILogger<ProductPage> Logger { get; set; } = default!;

    // private Toastify? Toastify { get; set; }
    // private Animations? Animations { get; set; }

    private bool IsLoading;
    private bool ShowDesactivateConfirm;
    private int PendingDesactivateId;
    private bool ShowCreateModal;
    private bool ShowEditModal;

    protected override async Task OnInitializedAsync()
    {
        ViewModel.OnFailure += HandleFailure;
        await ViewModel.InitializeViewModel();
        await ActionProductViewModel.EnsureCategoriesLoadedAsync();
    }

    private async Task OpenCreateModal()
    {
        await ActionProductViewModel.InitializeViewModel();
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
            var result = await ActionProductViewModel.CreateProductAsync();
            if (result)
            {
                ShowCreateModal = false;
                await ViewModel.InitializeViewModel();
                //await Tostify.ShowSuccessToast("Producto Creada Correctamente");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al crear categoría.");
            //await JsRuntime.InvokeVoidAsync("toastHelper.show", "Error inesperado al crear la categoria", "error", 3000);
        }
        finally
        {
            IsLoading = false;
        }
    }
    private async void OpenEditModal(ProductManager.ViewModels.Models.ProductModel category)
    {
        await ActionProductViewModel.EnsureCategoriesLoadedAsync();
        ActionProductViewModel.ProductInEdit(category);
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
            var result = await ActionProductViewModel.UpdateProductAsync();
            if (result)
            {
                ShowEditModal = false;
                await ViewModel.InitializeViewModel();
                //await Tostify.ShowSuccessToast("Categoria Actualizada Correctamente");
            }
        }
        catch (Exception ex)
        {
            //await JsRuntime.InvokeVoidAsync("toastHelper.show", "Error inesperado al actualizar la categoría", "error", 3000);
            Logger.LogError(ex, "Error al actualizar categoría.");
        }
        finally
        {
            IsLoading = false;
        }
    }
    private void AskDesactivate(int productId)
    {
        PendingDesactivateId = productId;
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
            await ViewModel.InitializeViewModel();
            ShowDesactivateConfirm = false;
            PendingDesactivateId = 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al desactivar producto");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void HandleFailure(object? sender, string errorMessage)
    {
        Logger.LogWarning("Fallo VM: {msg}", errorMessage);
    }

    public void Dispose()
    {
        ViewModel.OnFailure -= HandleFailure;
    }
}
