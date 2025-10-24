using CategoryManager.ViewModels.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryManager.Views.Components
{
    public partial class CreateCategoryForm
    {
        [Parameter] public CategoryModel Model { get; set; } = default!;
        [Parameter] public bool IsBusy { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [Parameter] public EventCallback OnValidSubmit { get; set; }
    }
}
