using CategoryManager.ViewModels.Resources;
using System.ComponentModel.DataAnnotations;

namespace CategoryManager.ViewModels.Models;
public class CategoryModel
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
    public bool IsActive { get; set; }

}
