using ProductManager.ViewModels.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.ViewModels.Models;

public class ProductModel
{
    public int Id { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = "RequiredName")]
    public string Name { get; set; }

    [Required(
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = "RequiredDescription")]
    public string Description { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = "RequiredField")]
    public int CategoryId { get; set; } = 0;
    public string CategoryName { get; set; }
    public bool IsActive { get; set; }
}
