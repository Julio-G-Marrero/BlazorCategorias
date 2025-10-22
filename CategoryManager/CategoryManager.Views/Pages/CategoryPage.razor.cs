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

    private bool ShowCreateModel;

    protected override async Task OnInitializedAsync()
    {
        ViewModel.OnFailure += HandleFailure;
        await ViewModel.InitializeViewModel();
    }

    private async void OpenCreateModal()
    {
       await  ActionCategoryViewModel.InitializeViewModel();
       ShowCreateModel = true;
    }

    private void CloseCreateModal()
    {
        ShowCreateModel = false;
    }
    private async Task SubmitCreateAsync()
    {
        try
        {
           IsLoading = true;
           bool result =  await ActionCategoryViewModel.CreateCategoryAsync();
           if (result)
           {
                ShowCreateModel = false;
                await ViewModel.LoadCategoriesAsync();
           }
        }
        catch(Exception ex)
        {
            Logger.LogError(ex, "Error occurred while creating category.");
            Console.WriteLine(ex.Message);
        }
        IsLoading = false;
    }

    private async void HandleFailure(object sender, string errorMessage)
    {
        Console.WriteLine(errorMessage);
    }

    public void Dispose()
    {
        ViewModel.OnFailure -= HandleFailure;
    }

}
