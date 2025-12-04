using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace UserManager.ViewModels.Models;

public class UserModel
{
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string UserName { get; set; }
    [Required, StringLength(100)]
    public string Surnames { get; set; }
    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; }
    [Required, StringLength(100)]
    public string Password { get; set; }
    public bool IsActive { get; set; }
}
