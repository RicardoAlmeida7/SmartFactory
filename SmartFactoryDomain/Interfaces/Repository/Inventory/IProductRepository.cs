using SmartFactoryDomain.Entities.Inventory;

namespace SmartFactoryDomain.Interfaces.Repository.Inventory
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(string category);
        Task<Product?> GetByCodeAsync(string code);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByCodeAsync(string code);
    }
}
