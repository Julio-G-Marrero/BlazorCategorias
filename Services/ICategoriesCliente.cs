using BlazorCategorias.Models;

namespace BlazorCategorias.Services
{
    public interface ICategoriesCliente
    {
        public Task<IEnumerable<Categorie>> GetCategories();
        public Task<Categorie> GetCategorie(int categorieId);
        public Task<Categorie> CreateCategorie(Categorie categoria);
        public Task<Categorie> UpdateCategorie(int categorieId, Categorie categoria);
        public Task<Categorie> DesactiveCategorie(int categorieId);
    }
}
