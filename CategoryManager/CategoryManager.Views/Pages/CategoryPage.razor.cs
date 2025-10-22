using CategoryManager.ViewModels.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace CategoryManager.Views.Pages;
public partial class CategoryPage : IDisposable
{
    [Inject]
    public SearchCategoryViewModel ViewModel { get; set; }
    
    [Inject]
    public ActionCategoryViewModel ActionCategoryViewModel { get; set; }

    [Inject]
    public ILogger<CategoryPage> Logger { get; set; }


    private bool IsLoading;
    private bool ShowFormModal;
    private bool IsEditMode;
    private bool ShowDesactivateConfirm;
    private int PendingDesactivateId;
    protected override async Task OnInitializedAsync()
    {
        ViewModel.OnFailure += HandleFailure;
        await ViewModel.InitializeViewModel();
    }

    private async void OpenCreateModal()
    {
        await  ActionCategoryViewModel.InitializeViewModel();
        ShowFormModal = true;
        IsEditMode = false;
    }

    private void CloseFormModal() => ShowFormModal = false;
    private void CancelDesactivate() => ShowDesactivateConfirm = false;

    private async Task SubmitFormAsync()
    {
        try
        {
            IsLoading = true;
            bool actionBool = IsEditMode
                ? await ActionCategoryViewModel.UpdateCategoryAsync()
                : await ActionCategoryViewModel.CreateCategoryAsync();

            if (actionBool)
            {
                ShowFormModal = false;
                await ViewModel.InitializeViewModel();
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al guardar categoria.");
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
        IsEditMode = true;
        ActionCategoryViewModel.CategoryInEdit(category);
        ShowFormModal = true;
    }
    private void AskDesactivate(int id)
    {
        PendingDesactivateId = id;
        ShowDesactivateConfirm = true;
    }

    private async Task ConfirmDesactivateAsync()
    {
        try
        {
            IsLoading = true;
            bool actionBool = await ActionCategoryViewModel.DesactivateCategoryAsync(PendingDesactivateId);
            ShowDesactivateConfirm = false;

            if (actionBool)
            {
                await ViewModel.InitializeViewModel(); // refresca la tabla
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al eliminar categoria {Id}.", PendingDesactivateId);
        }
        finally
        {
            IsLoading = false;
        }
    }

}
