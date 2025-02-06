using SmartFactoryApplication.Inventory.Models;
using SmartFactoryDomain.Enums;

namespace SmartFactoryApplication.Inventory.Interfaces.Services
{
    public interface IStockMovementService
    {
        Task<StockMovementModel> CreateStockMovementAsync(int materialId, int quantity, MovementType type, string description);
        Task<IEnumerable<StockMovementModel>> GetByMaterialIdAsync(int materialId);
        Task<IEnumerable<StockMovementModel>> GetByMovementTypeAsync(MovementType type);
        Task<IEnumerable<StockMovementModel>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
