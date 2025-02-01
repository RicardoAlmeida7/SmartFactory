using AutoMapper;
using SmartFactoryApplication.Inventory.Interfaces;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

namespace SmartFactoryApplication.Inventory.Service
{
    public class MaterialService(IMaterialRepository materialRepository, IMapper mapper) : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository = materialRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<MaterialDto> CreateMaterialAsync(string name, string code, decimal unitPrice, int stockQuantity, string unitOfMeasure)
        {
            var material = new Material(name, code, unitPrice, stockQuantity, unitOfMeasure);
            var createdMaterial = await _materialRepository.CreateAsync(material);
            return _mapper.Map<MaterialDto>(createdMaterial);
        }

        public async Task<bool> DeleteMaterialAsync(int id)
        {
            var material = await _materialRepository.GetByIdAsync(id);
            if (material == null) return false;
            return await _materialRepository.DeleteAsync(material);
        }

        public async Task<IEnumerable<MaterialDto>> GetAllMaterialsAsync()
        {
            var materials = await _materialRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MaterialDto>>(materials);
        }

        public async Task<MaterialDto?> GetMaterialByIdAsync(int id)
        {
            var material = await _materialRepository.GetByIdAsync(id);
            return material == null ? null : _mapper.Map<MaterialDto>(material);
        }

        public async Task<MaterialDto> UpdateMaterialAsync(int id, string name, string code, decimal unitPrice, int stockQuantity, string unitOfMeasure)
        {
            var material = new Material(name, code, unitPrice, stockQuantity, unitOfMeasure) { Id = id };
            var updatedMaterial = await _materialRepository.UpdateAsync(material);
            return _mapper.Map<MaterialDto>(updatedMaterial);
        }
    }
}
