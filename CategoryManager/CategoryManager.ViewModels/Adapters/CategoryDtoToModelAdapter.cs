using CategoryManager.ViewModels.Models;
using Domain.Categories;

namespace CategoryManager.ViewModels.Adapters;
internal static class CategoryDtoToModelAdapter
{
    public static CategoryModel ToModel(this CategoryDto dto)
    {
        return new CategoryModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive
        };
    }

    public static List<CategoryModel> ToModelList(this IEnumerable<CategoryDto> dtoList)
    {
        List<CategoryModel> categories = new List<CategoryModel>();
        if (dtoList != null)
        {
            categories = dtoList.Select(dto => dto.ToModel()).ToList();
        }
        return categories;
    }

    public static CategoryDto ToDto(this CategoryModel model)
    {
        return new CategoryDto
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            IsActive = model.IsActive
        };
    }

}
