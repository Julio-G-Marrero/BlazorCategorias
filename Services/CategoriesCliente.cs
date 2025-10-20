using BlazorCategorias.Models;
using System.Text.Json;
using System.Text;
namespace BlazorCategorias.Services
{
    public class CategoriesCliente : ICategoriesCliente
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlBase;
        public CategoriesCliente(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _urlBase = configuration["Api:BaseUrl"]!;
        }
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            //PropertyNameCaseInsensitive = true
        };
        public async Task<Categorie?> CreateCategorie(Categorie categorie)
        {
            var content = JsonSerializer.Serialize(categorie);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_urlBase}/api/categories", bodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var api = JsonSerializer.Deserialize<ApiResponse<Categorie>>(responseContent, _jsonOptions);

            if (!response.IsSuccessStatusCode || api is null || !api.Success)
                throw new Exception(api?.ErrorMessage ?? "Error al crear la categoría");

            return api.SuccessValue;
        }

        public async Task<Categorie> DesactiveCategorie(int categorieId)
        {
            var response = await _httpClient.DeleteAsync($"{_urlBase}/api/categories/{categorieId}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var categorie = JsonSerializer.Deserialize<Categorie>(content, _jsonOptions);
                return categorie!;
            }
            else
            {
                var err = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                throw new Exception(err?.ErrorMessage ?? "Error al obtener la categoría");
            }
        }

        public async Task<Categorie> GetCategorie(int categorieId)
        {
            var response = await _httpClient.GetAsync($"{_urlBase}/api/categories/{categorieId}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var categorie = JsonSerializer.Deserialize<Categorie>(content, _jsonOptions);

                return categorie!;
            }
            else
            {
                var err = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                throw new Exception(err?.ErrorMessage ?? "Error al obtener la categoría");
            }
        }

        public async Task<IEnumerable<Categorie>> GetCategories()
        {
            var response = await _httpClient.GetAsync($"{_urlBase}/api/categories");
            var content = await response.Content.ReadAsStringAsync();


            var api = JsonSerializer.Deserialize<ApiResponse<List<Categorie>>>(content, _jsonOptions);

            if (!response.IsSuccessStatusCode || api is null || !api.Success)
                throw new Exception("Error al obtener las categorías");

            return api.SuccessValue ?? new List<Categorie>();
        }


        public async Task<Categorie> UpdateCategorie(int categorieId, Categorie categorie)
        {
            var payload = new
            {
                id = categorieId,
                name = categorie.Name,
                description = categorie.Description
            };

            var bodyJson = JsonSerializer.Serialize(payload);
            using var body = new StringContent(bodyJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_urlBase}/api/categories", body);
            var content = await response.Content.ReadAsStringAsync();

            var api = JsonSerializer.Deserialize<ApiResponse<Categorie>>(content, _jsonOptions);

            if (!response.IsSuccessStatusCode || api is null || !api.Success)
                throw new Exception(api?.ErrorMessage ?? "Error al actualizar la categoría");

            return api.SuccessValue!;
        }
    }
}
