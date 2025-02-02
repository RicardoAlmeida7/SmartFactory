using SmartFactoryApplication.Inventory.Models;

namespace SmartFactoryApplication.Inventory.Interfaces
{
    public interface IProductMaterialService
    {
        Task<IEnumerable<ProductMaterialModel>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ProductMaterialModel>> GetByMaterialIdAsync(int materialId);
        Task<IEnumerable<ProductMaterialModel>> GetAllProductMaterialsAsync();
    }
}
