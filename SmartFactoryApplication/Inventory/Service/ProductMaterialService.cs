using AutoMapper;
using SmartFactoryApplication.Inventory.Interfaces;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

namespace SmartFactoryApplication.Inventory.Service
{
    public class ProductMaterialService(IProductMaterialRepository productMaterialRepository, IMapper mapper) : IProductMaterialService
    {
        private readonly IProductMaterialRepository _productMaterialRepository = productMaterialRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ProductMaterialDto>> GetAllProductMaterialsAsync()
        {
            var productMaterial = await _productMaterialRepository.GetAllAsync();
            return (IEnumerable<ProductMaterialDto>)_mapper.Map<ProductMaterialDto>(productMaterial);
        }

        public async Task<IEnumerable<ProductMaterialDto>> GetByMaterialIdAsync(int materialId)
        {
            var productMaterial = await _productMaterialRepository.GetByMaterialIdAsync(materialId);
            return (IEnumerable<ProductMaterialDto>)_mapper.Map<ProductMaterialDto>(productMaterial);
        }

        public async Task<IEnumerable<ProductMaterialDto>> GetByProductIdAsync(int productId)
        {
            var productMaterial = await _productMaterialRepository.GetByProductIdAsync(productId);
            return (IEnumerable<ProductMaterialDto>)_mapper.Map<ProductMaterialDto>(productMaterial);
        }
    }
}
