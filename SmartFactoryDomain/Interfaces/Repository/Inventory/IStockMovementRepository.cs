using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Enums;

namespace SmartFactoryDomain.Interfaces.Repository.Inventory
{
    public interface IStockMovementRepository: IBaseRepository<StockMovementBase>
    {
        Task<IEnumerable<StockMovementBase>> GetByMaterialIdAsync(int materialId);
        Task<IEnumerable<StockMovementBase>> GetByMovementTypeAsync(MovementType type);
        Task<IEnumerable<StockMovementBase>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
