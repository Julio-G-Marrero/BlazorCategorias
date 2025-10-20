using Newtonsoft.Json;

namespace BlazorCategorias.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        [JsonProperty("successValue")]
        public T? SuccessValue { get; set; }
    }
}
