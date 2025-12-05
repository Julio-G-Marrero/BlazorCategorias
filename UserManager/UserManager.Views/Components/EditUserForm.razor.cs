using Microsoft.AspNetCore.Components;
using UserManager.ViewModels.Models;

namespace UserManager.Views.Components;

public partial class EditUserForm : ComponentBase
{
    [Parameter] public UserModel? Model { get; set; }
    [Parameter] public bool IsBusy { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnValidSubmit { get; set; }

    protected override void OnParametersSet()
    {
        Model ??= new UserModel();
        Model.NewPassword ??= string.Empty;
    }
}
