using CategoryManager.ViewModels.ViewModels;
using Common.Views.Animations;
using Common.Views.Tostify;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
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

    public IJSRuntime JsRuntime { get; set; }

    private bool IsLoading;
    private bool ShowDesactivateConfirm;
    private int PendingDesactivateId;
    private bool ShowCreateModal;
    private bool ShowEditModal;

    private Tostify Tostify;
    private Animations Animations;
    protected override async Task OnInitializedAsync()
    {
        ViewModel.OnFailure += HandleFailure;
        await ViewModel.InitializeViewModel();
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (ShowCreateModal && Animations != null)
            await Animations.AnimateModalOpenAsync("#createModal");

        if (ShowEditModal && Animations != null)
            await Animations.AnimateModalOpenAsync("#editModal");

        if (ShowDesactivateConfirm && Animations != null)
            await Animations.AnimateModalOpenAsync("#confirmModal");
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
                await Tostify.ShowSuccessToast("Categoria Creada Correctamente");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al crear categoría.");
            await JsRuntime.InvokeVoidAsync("toastHelper.show", "Error inesperado al crear la categoria", "error", 3000);
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
                await Tostify.ShowSuccessToast("Categoria Actualizada Correctamente");
            }
        }
        catch (Exception ex)
        {
            await JsRuntime.InvokeVoidAsync("toastHelper.show", "Error inesperado al actualizar la categoría", "error", 3000);
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
    private async void CloseEditModal() { 
        ShowEditModal = false;
    }


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
            ShowDesactivateConfirm = false;
            if (Animations != null)
            {
                await Animations.FadeOutRowAsync($"#row-{PendingDesactivateId}");
            }
            var result = await ActionCategoryViewModel.DesactivateCategoryAsync(PendingDesactivateId);
            if (result)
            {
                await ViewModel.InitializeViewModel();
                if (Tostify != null)
                {
                    await Tostify.ShowSuccessToast("Categoria Desactivada Correctamente");
                }

            }

        }
        catch (Exception ex)
        {
            await JsRuntime.InvokeVoidAsync("toastHelper.show", "Error al desactivar categoría", "error", 3000);
        }
        finally
        {
            IsLoading = false;
        }
    }

}
