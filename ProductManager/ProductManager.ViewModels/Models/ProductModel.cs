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
    [Required, StringLength(100)]
    public string Name { get; set; }
    [Required, StringLength(100)]
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public bool IsActive { get; set; }
}
