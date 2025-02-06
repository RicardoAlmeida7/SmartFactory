using SmartFactoryApplication.Inventory.Models;

namespace SmartFactoryApplication.Inventory.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductModel> CreateProductAsync(ProductModel model);
        Task<ProductModel?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductModel>> GetAllProductsAsync();
        Task<ProductModel> UpdateProductAsync(ProductModel model);
        Task<bool> DeleteProductAsync(int id);
    }
}
