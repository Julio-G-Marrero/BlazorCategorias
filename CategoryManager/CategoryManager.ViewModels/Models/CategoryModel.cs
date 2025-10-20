using System.ComponentModel.DataAnnotations;

namespace CategoryManager.ViewModels.Models;
public class CategoryModel
{
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string Name { get; set; }
    [Required, StringLength(100)]
    public string Description { get; set; }
    public bool IsActive { get; set; }

}
