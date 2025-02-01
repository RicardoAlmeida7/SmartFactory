using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Enums;

namespace SmartFactoryDomain.Interfaces.Repository.Inventory
{
    public interface IStockMovementRepository: IBaseRepository<StockMovement>
    {
        Task<IEnumerable<StockMovement>> GetByMaterialIdAsync(int materialId);
        Task<IEnumerable<StockMovement>> GetByMovementTypeAsync(MovementType type);
        Task<IEnumerable<StockMovement>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
