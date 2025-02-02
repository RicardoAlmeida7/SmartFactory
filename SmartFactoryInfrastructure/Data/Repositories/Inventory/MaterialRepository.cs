using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

namespace SmartFactoryInfrastructure.Data.Repositories.Inventory
{
    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
        public Task<Material?> GetByCodeAsync(string code)
        {
            var material = _storage.Values
                            .OfType<Material>()
                            .FirstOrDefault(m => m.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(material);
        }

        public Task<IEnumerable<Material>> GetByProductIdAsync(int productId)
        {
            var material = _storage.Values
                            .OfType<Material>()
                            .Where(m => m.Id.Equals(productId))
                            .ToList();

            return Task.FromResult<IEnumerable<Material>>(material);
        }
    }
}
