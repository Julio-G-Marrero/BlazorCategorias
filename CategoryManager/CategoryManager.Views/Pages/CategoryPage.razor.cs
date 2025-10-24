using CategoryManager.ViewModels.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Domain.Application.Abstractions;
namespace CategoryManager.Views.Pages;
public partial class CategoryPage : IDisposable
{
    [Inject]
    public SearchCategoryViewModel ViewModel { get; set; }

    [Inject]
    public ActionCategoryViewModel ActionCategoryViewModel { get; set; }

    [Inject]
    public ILogger<CategoryPage> Logger { get; set; }

    [Inject] 
    public IToastService Toast { get; set; } = default!;

    private bool IsLoading;
    private bool ShowDesactivateConfirm;
    private int PendingDesactivateId;
    private bool ShowCreateModal;
    private bool ShowEditModal;

    protected override async Task OnInitializedAsync()
    {
        ViewModel.OnFailure += HandleFailure;
        await ViewModel.InitializeViewModel();
    }

    private async Task OpenCreateModal()
    {
        await ActionCategoryViewModel.InitializeViewModel();
        ShowCreateModal = true;
    }


    private async Task CreateAsync()
    {
        try
        {
            IsLoading = true;
            var result = await ActionCategoryViewModel.CreateCategoryAsync();
            if (result)
            {
                ShowCreateModal = false;
                await ViewModel.InitializeViewModel();
                await Toast.ShowInfoToast("Categoria creada correctamente", "success");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al crear categoría.");
            await Toast.ShowInfoToast("Error inesperado al crear la categoria", "error");

        }
        finally
        {
            IsLoading = false;
        }
    }
    private async Task UpdateAsync()
    {
        try
        {
            IsLoading = true;
            var result = await ActionCategoryViewModel.UpdateCategoryAsync();
            if (result)
            {
                ShowEditModal = false;
                await ViewModel.InitializeViewModel();

            }
        }
        catch (Exception ex)
        {
            await Toast.ShowInfoToast("Error inesperado al actualizar la categoría", "error");
            Logger.LogError(ex, "Error al actualizar categoría.");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async void HandleFailure(object? sender, string errorMessage)
    {
        Console.WriteLine(errorMessage);
    }

    public void Dispose()
    {
        ViewModel.OnFailure -= HandleFailure;
    }

    private void OpenEditModal(CategoryManager.ViewModels.Models.CategoryModel category)
    {
        ActionCategoryViewModel.CategoryInEdit(category);
        ShowEditModal = true;
    }
    private void CloseEditModal() => ShowEditModal = false;

    private void AskDesactivate(int id)
    {
        PendingDesactivateId = id;
        ShowDesactivateConfirm = true;
    }
    private void CancelDesactivate() => ShowDesactivateConfirm = false;
    private async Task ConfirmDesactivateAsync()
    {
        try
        {
            IsLoading = true;
            var result = await ActionCategoryViewModel.DesactivateCategoryAsync(PendingDesactivateId);
            ShowDesactivateConfirm = false;
            if (result)
            {
                await ViewModel.InitializeViewModel();
            }

        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al desactivar categoría {Id}.", PendingDesactivateId);
            await Toast.ShowInfoToast("Error inesperado al desactivar la categoría", "error");
        }
        finally
        {
            IsLoading = false;
        }
    }

}
