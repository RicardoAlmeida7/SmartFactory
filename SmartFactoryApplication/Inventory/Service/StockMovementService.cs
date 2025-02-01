using AutoMapper;
using SmartFactoryApplication.Inventory.Interfaces;
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

        public async Task<StockMovementDto> CreateStockMovementAsync(int materialId, int quantity, MovementType type, string description)
        {
            var stockMovement = new StockMovement(materialId, quantity, type, description);
            var createdStockMovement = await _stockMovementRepository.CreateAsync(stockMovement);
            return _mapper.Map<StockMovementDto>(createdStockMovement);
        }

        public async Task<IEnumerable<StockMovementDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var stockMovement = await _stockMovementRepository.GetByDateRangeAsync(startDate, endDate);
            return (IEnumerable<StockMovementDto>)_mapper.Map<StockMovementDto>(stockMovement);
        }

        public async Task<IEnumerable<StockMovementDto>> GetByMaterialIdAsync(int materialId)
        {
            var stockMovement = await _stockMovementRepository.GetByMaterialIdAsync(materialId);
            return (IEnumerable<StockMovementDto>)_mapper.Map<StockMovementDto>(stockMovement);
        }

        public async Task<IEnumerable<StockMovementDto>> GetByMovementTypeAsync(MovementType type)
        {
            var stockMovement = await _stockMovementRepository.GetByMovementTypeAsync(type);
            return (IEnumerable<StockMovementDto>)_mapper.Map<StockMovementDto>(stockMovement);
        }
    }
}
