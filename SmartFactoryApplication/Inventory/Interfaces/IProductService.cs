using SmartFactoryApplication.Inventory.Models;

namespace SmartFactoryApplication.Inventory.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(string name, string code, string category, decimal price, int stockQuantity);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> UpdateProductAsync(int id, string name, string code, string category, decimal price, int stockQuantity);
        Task<bool> DeleteProductAsync(int id);
    }
}
