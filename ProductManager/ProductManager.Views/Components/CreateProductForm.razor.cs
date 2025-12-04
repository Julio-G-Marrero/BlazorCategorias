using Domain.Categories;
using Microsoft.AspNetCore.Components;
using ProductManager.ViewModels.Models;

namespace ProductManager.Views.Components;

public partial class CreateProductForm
{
    [Parameter] public ProductModel Model { get; set; } = default!;
    [Parameter] public bool IsBusy { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public IEnumerable<CategoryDto> Categories { get; set; } = Enumerable.Empty<CategoryDto>();

}