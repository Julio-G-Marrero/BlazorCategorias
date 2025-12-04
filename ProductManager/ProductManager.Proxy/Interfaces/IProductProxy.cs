using Domain.Products;
using Domain.ValueObjects;

namespace ProductManager.Proxy.Interfaces;

public interface IProductProxy
{
    Task<HandlerRequestResult<IEnumerable<ProductDto>>> GetAllProductAsync();

    Task<HandlerRequestResult> AddProductAsync(ProductDto dto);

    Task<HandlerRequestResult> UpdateProductAsync(ProductDto dto);

    Task<HandlerRequestResult> DesactivateProductAsync(int id);
}
