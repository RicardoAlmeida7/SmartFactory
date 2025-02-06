using SmartFactoryDomain.Entities.Inventory;

namespace SmartFactoryDomain.Interfaces.Repository.Inventory
{
    public interface IMaterialRepository : IBaseRepository<Material>
    {
        Task<IEnumerable<Material>> GetByProductIdAsync(int productId);
        Task<Material?> GetByCodeAsync(string code);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByCodeAsync(string code);
    }
}
