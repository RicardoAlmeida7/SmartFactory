using SmartFactoryApplication.Inventory.Models;
using SmartFactoryDomain.Enums;

namespace SmartFactoryApplication.Inventory.Interfaces
{
    public interface IStockMovementService
    {
        Task<StockMovementDto> CreateStockMovementAsync(int materialId, int quantity, MovementType type, string description);
        Task<IEnumerable<StockMovementDto>> GetByMaterialIdAsync(int materialId);
        Task<IEnumerable<StockMovementDto>> GetByMovementTypeAsync(MovementType type);
        Task<IEnumerable<StockMovementDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
