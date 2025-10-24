namespace BlazorCategorias.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public T? SuccessValue { get; set; }
    }
}
