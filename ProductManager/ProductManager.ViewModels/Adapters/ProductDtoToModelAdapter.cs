using Domain.Categories;
using Domain.Products;
using ProductManager.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.ViewModels.Adapters;
internal static class ProductDtoToModelAdapter
{
    public static ProductModel ToModel(this ProductDto dto)
    {
        return new ProductModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            CategoryName = dto.CategoryName,
            IsActive = dto.IsActive
        };
    }

    public static List<ProductModel> ToModelList(this IEnumerable<ProductDto> dtoList)
    {
        List<ProductModel> products = new List<ProductModel>();
        if(dtoList != null)
        {
            products = dtoList.Select(dto => dto.ToModel()).ToList();
        }
        return products;
    }

    public static ProductDto ToDto(this ProductModel model)
    {
        return new ProductDto
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            CategoryId = model.CategoryId,
            CategoryName = model.CategoryName,
            IsActive = model.IsActive
        };
    }
}
