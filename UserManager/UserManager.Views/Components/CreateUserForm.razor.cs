using Microsoft.AspNetCore.Components;
using UserManager.ViewModels.Models;

namespace UserManager.Views.Components;

public partial class CreateUserForm
{
    [Parameter] public UserModel Model { get; set; } = default!;
    [Parameter] public bool IsBusy { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnValidSubmit { get; set; }
}
