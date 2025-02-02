using SmartFactoryApplication.Inventory.Models;

namespace SmartFactoryApplication.Inventory.Interfaces
{
    public interface IProductService
    {
        Task<ProductModel> CreateProductAsync(string name, string code, string category, decimal price, int stockQuantity);
        Task<ProductModel?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductModel>> GetAllProductsAsync();
        Task<ProductModel> UpdateProductAsync(int id, string name, string code, string category, decimal price, int stockQuantity);
        Task<bool> DeleteProductAsync(int id);
    }
}
