using Microsoft.EntityFrameworkCore;
using SmartFactoryData.Context;
using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

namespace SmartFactoryData.Repositories.Inventory
{
    public class MaterialRepository(AppDbContext context) : BaseRepository<Material>(context), IMaterialRepository
    {
        public async Task<bool> ExistsByCodeAsync(string code) =>
            await _dbSet.Where(m => m.Code.ToLower() == code.ToLower()).AnyAsync();

        public async Task<bool> ExistsByNameAsync(string name) =>
            await _dbSet.Where(m => m.Name.ToLower() == name.ToLower()).AnyAsync();

        public async Task<Material?> GetByCodeAsync(string code)
        {
            return await _dbSet.AsNoTracking()
                               .FirstOrDefaultAsync(m => m.Code == code);
        }

        public async Task<IEnumerable<Material>> GetByProductIdAsync(int productId)
        {
            return await _dbSet.AsNoTracking()
                               .Where(m => m.Id == productId)
                               .ToListAsync();
        }
    }
}
