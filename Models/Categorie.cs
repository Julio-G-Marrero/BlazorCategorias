using System.ComponentModel.DataAnnotations;

namespace BlazorCategorias.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
