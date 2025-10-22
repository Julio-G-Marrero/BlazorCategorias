using CategoryManager.ViewModels.ViewModels;
using Microsoft.AspNetCore.Components;
namespace CategoryManager.Views.Pages;
public partial class CategoryPage : ComponentBase, IDisposable
{
    [Inject]
    public SearchCategoryViewModel ViewModel { get; set; }
    [Inject]
    public ActionCategoryViewModel ActionCategoryVM { get; set; }
    private bool _showCreateModal;

    protected override async Task OnInitializedAsync()
    {
        ViewModel.OnFailure += HandleFailure;
        ActionCategoryVM.OnCategorySaved += HandleCategorySaved;
        await ViewModel.InitializeViewModel();
    }
    private async void HandleCategorySaved(object? sender, EventArgs e)
    {
        await ViewModel.InitializeViewModel();
        _showCreateModal = false;
        await InvokeAsync(StateHasChanged);
    }
    private void OpenCreateModal()
    {
        ActionCategoryVM.Category = new CategoryManager.ViewModels.Models.CategoryModel();
        _showCreateModal = true;
    }

    private void CloseCreateModal()
    {
        _showCreateModal = false;
        StateHasChanged();
    }
    private async Task SubmitCreateAsync()
    {
        await ActionCategoryVM.SaveAsync();
    }

    private async void HandleFailure(object sender, string errorMessage)
    {
        Console.WriteLine(errorMessage);
    }

    public void Dispose()
    {
        ViewModel.OnFailure -= HandleFailure;
        ActionCategoryVM.OnCategorySaved -= HandleCategorySaved;
    }

}
