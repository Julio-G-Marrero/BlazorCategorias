using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using UserManager.ViewModels.Resources;

namespace UserManager.ViewModels.Models;

public class UserModel
{
    public int Id { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = "RequiredUserName")]
    public string UserName { get; set; }

    [Required(
         ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = "RequiredName")]
    public string Surnames { get; set; }

    [Required(
     ErrorMessageResourceType = typeof(ValidationMessages),
     ErrorMessageResourceName = "RequiredEmail")]
    public string Email { get; set; }

    [Required(
      ErrorMessageResourceType = typeof(ValidationMessages),
      ErrorMessageResourceName = "RequiredPassword")]
    public string Password { get; set; }

    public string? NewPassword { get; set; }
    public bool IsActive { get; set; }
}
