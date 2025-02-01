using SmartFactoryDomain.Entities.Inventory;

namespace SmartFactoryDomain.Interfaces.Repository.Inventory
{
    public interface IProductMaterialRepository : IBaseRepository<ProductMaterial>
    {
        Task<IEnumerable<ProductMaterial>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ProductMaterial>> GetByMaterialIdAsync(int materialId);
    }
}
