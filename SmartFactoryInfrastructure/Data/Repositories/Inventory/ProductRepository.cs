using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

namespace SmartFactoryInfrastructure.Data.Repositories.Inventory
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            var products = _storage.Values
                            .OfType<Product>()
                            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                            .ToList();

            return Task.FromResult<IEnumerable<Product>>(products);
        }

        public Task<Product?> GetByCodeAsync(string code)
        {
            var product = _storage.Values
                            .OfType<Product>()
                            .FirstOrDefault(p => p.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(product);
        }
    }
}
