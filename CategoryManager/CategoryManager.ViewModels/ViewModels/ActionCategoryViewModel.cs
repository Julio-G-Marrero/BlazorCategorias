using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using CategoryManager.Proxy;
using CategoryManager.ViewModels.Adapters;
using CategoryManager.ViewModels.Models;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace CategoryManager.ViewModels.ViewModels;

public class ActionCategoryViewModel(CategoryProxy proxy, ILogger<ActionCategoryViewModel> logger) : INotifyPropertyChanged
{
    private CategoryModel _category = new();
    public CategoryModel Category
    {
        get => _category;
        set { _category = value; OnChanged(); }
    }

    private bool _isSaving;
    public bool IsSaving
    {
        get => _isSaving;
        private set { _isSaving = value; OnChanged(); }
    }

    private string? _errorMessage;
    public string? ErrorMessage
    {
        get => _errorMessage;
        private set { _errorMessage = value; OnChanged(); }
    }

    private string? _successMessage;
    public string? SuccessMessage
    {
        get => _successMessage;
        private set { _successMessage = value; OnChanged(); }
    }

    public event EventHandler<string>? OnFailure;
    public event EventHandler<string>? OnSuccess;
    public event EventHandler OnCategorySaved;

    public async Task SaveAsync()
    {
        ErrorMessage = SuccessMessage = null;

        var validationContext = new ValidationContext(Category);
        var results = new List<ValidationResult>();
        if (!Validator.TryValidateObject(Category, validationContext, results, validateAllProperties: true))
        {
            ErrorMessage = string.Join(" | ", results.Select(r => r.ErrorMessage));
            OnFailure?.Invoke(this, ErrorMessage);
            return;
        }

        IsSaving = true;
        try
        {
            var dto = Category.ToDto();

            HandlerRequestResult result = await proxy.AddCategoryAsync(dto);

            if (result.Success)
            {
                SuccessMessage = "Categoría creada exitosamente";
                OnSuccess?.Invoke(this, SuccessMessage);
                 OnCategorySaved?.Invoke(this, EventArgs.Empty);
                Category = new CategoryModel();
            }
            else
            {
                ErrorMessage = result.ErrorMessage ?? "Error al crear la categoría";
                OnFailure?.Invoke(this, ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error inesperado al crear categoria");
            ErrorMessage = "Ocurrio un error inesperado.";
            OnFailure?.Invoke(this, ErrorMessage);
        }
        finally
        {
            IsSaving = false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnChanged([CallerMemberName] string? n = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}
