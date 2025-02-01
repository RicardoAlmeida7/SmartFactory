using SmartFactoryApplication.Inventory.Models;

namespace SmartFactoryApplication.Inventory.Interfaces
{
    public interface IProductMaterialService
    {
        Task<IEnumerable<ProductMaterialDto>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ProductMaterialDto>> GetByMaterialIdAsync(int materialId);
        Task<IEnumerable<ProductMaterialDto>> GetAllProductMaterialsAsync();
    }
}
