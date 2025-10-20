using CategoryManager.ViewModels.ViewModels;
using Microsoft.AspNetCore.Components;

namespace CategoryManager.Views.Pages;
public partial class CategoryPage : IDisposable
{
    [Inject]
    public SearchCategoryViewModel ViewModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ViewModel.OnFailure += HandleFailure;
        await ViewModel.InitializeViewModel();
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
