using AutoMapper;
using SmartFactoryApplication.Inventory.Interfaces.Services;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Enums;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

namespace SmartFactoryApplication.Inventory.Service
{
    public class StockMovementService(IStockMovementRepository stockMovementRepository, IMapper mapper) : IStockMovementService
    {
        private readonly IStockMovementRepository _stockMovementRepository = stockMovementRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<StockMovementModel> CreateStockMovementAsync(int materialId, int quantity, MovementType type, string description)
        {
            var stockMovement = new ProductStockMovement(materialId, quantity, type, description);
            var createdStockMovement = await _stockMovementRepository.CreateAsync(stockMovement);
            return _mapper.Map<StockMovementModel>(createdStockMovement);
        }

        public async Task<IEnumerable<StockMovementModel>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var stockMovement = await _stockMovementRepository.GetByDateRangeAsync(startDate, endDate);
            return (IEnumerable<StockMovementModel>)_mapper.Map<StockMovementModel>(stockMovement);
        }

        public async Task<IEnumerable<StockMovementModel>> GetByMaterialIdAsync(int materialId)
        {
            var stockMovement = await _stockMovementRepository.GetByMaterialIdAsync(materialId);
            return (IEnumerable<StockMovementModel>)_mapper.Map<StockMovementModel>(stockMovement);
        }

        public async Task<IEnumerable<StockMovementModel>> GetByMovementTypeAsync(MovementType type)
        {
            var stockMovement = await _stockMovementRepository.GetByMovementTypeAsync(type);
            return (IEnumerable<StockMovementModel>)_mapper.Map<StockMovementModel>(stockMovement);
        }
    }
}
