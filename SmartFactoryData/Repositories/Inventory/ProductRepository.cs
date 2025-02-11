using Microsoft.EntityFrameworkCore;
using SmartFactoryData.Context;
using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

namespace SmartFactoryData.Repositories.Inventory
{
    public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        public async Task<bool> ExistsByCodeAsync(string code) =>
             await _dbSet.Where(m => m.Code.ToLower() == code.ToLower()).AnyAsync();

        public async Task<bool> ExistsByNameAsync(string name) =>
            await _dbSet.Where(m => m.Name.ToLower() == name.ToLower()).AnyAsync();

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            return await _dbSet.AsNoTracking()
                               .Where(p => p.Category == category)
                               .ToListAsync();
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await _dbSet.AsNoTracking()
                               .FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}
