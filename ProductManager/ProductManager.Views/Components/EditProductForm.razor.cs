using Microsoft.AspNetCore.Components;
using ProductManager.ViewModels.Models;
using System.Collections.Generic;
using System.Linq;
using Domain.Categories;
namespace ProductManager.Views.Components;

public partial class EditProductForm : ComponentBase
{
    [Parameter] public ProductModel? Model { get; set; }
    [Parameter] public bool IsBusy { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnValidSubmit { get; set; }

    [Parameter] public IEnumerable<CategoryDto> Categories { get; set; } = Enumerable.Empty<CategoryDto>();

    protected override void OnParametersSet()
    {
        Model ??= new ProductModel();
    }
}
