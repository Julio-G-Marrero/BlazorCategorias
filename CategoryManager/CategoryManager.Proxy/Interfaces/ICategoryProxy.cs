using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Categories;
using Domain.ValueObjects;

namespace CategoryManager.Proxy.Interfaces;

public interface ICategoryProxy
{
    Task<HandlerRequestResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync();
    Task<HandlerRequestResult> AddCategoryAsync(CategoryDto dto);
    Task<HandlerRequestResult> UpdateCategoryAsync(CategoryDto dto);
    Task<HandlerRequestResult> DesactivateCategoryAsync(int id);
}
