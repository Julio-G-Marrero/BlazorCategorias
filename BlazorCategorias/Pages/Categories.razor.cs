using BlazorCategorias.Models;

namespace BlazorCategorias.Pages
{
    public partial class Categories
    {
        List<Categorie> categories = new();
        Categorie formModel = new();

        private bool IsLoading = true;
        private bool ShowForm = false;
        private bool IsEdit = false;
        private bool IsSaving = false;

        private string? SuccessMessage;
        private string? ErrorMessage;
        private string? FormError;

        protected override async Task OnInitializedAsync()
        {
            await UploadCategories();
        }

        private async Task UploadCategories()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = null;
                categories = (await CategoriesClient.GetCategories()).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }

        void NewCategorie()
        {
            formModel = new Categorie { IsActive = true };
            IsEdit = false;
            FormError = null;
            SuccessMessage = null;
            ShowForm = true;
        }

        void EditCategorie(Categorie categorie)
        {
            formModel = new Categorie
            {
                Id = categorie.Id,
                Name = categorie.Name,
                Description = categorie.Description,
                IsActive = categorie.IsActive,
            };
            IsEdit = true;
            FormError = null;
            SuccessMessage = null;
            ShowForm = true;
        }

        void CloseModal()
        {
            ShowForm = false;
        }

        async Task SubmitFormAsync(Categorie data)
        {
            try
            {
                IsSaving = true;
                FormError = null;

                if (!IsEdit)
                {
                    var creada = await CategoriesClient.CreateCategorie(data);
                    if (creada is not null)
                    {
                        await UploadCategories();
                        SuccessMessage = "Categoría creada correctamente.";
                    }
                }
                else
                {
                    var actualizada = await CategoriesClient.UpdateCategorie(data.Id, data);
                    await UploadCategories();
                    ShowForm = false;
                    StateHasChanged();
                    SuccessMessage = "Categoría actualizada correctamente.";

                }

                ShowForm = false;
            }
            catch (Exception ex)
            {
                FormError = ex.Message;
            }
            finally
            {
                IsSaving = false;
            }
        }

        async Task DesactivateCategorie(int id)
        {
            try
            {
                var cat = await CategoriesClient.DesactiveCategorie(id);
                await UploadCategories();
                SuccessMessage = "Categoría desactivada.";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

    }
}
