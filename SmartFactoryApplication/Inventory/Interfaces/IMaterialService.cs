using SmartFactoryApplication.Inventory.Models;

namespace SmartFactoryApplication.Inventory.Interfaces
{
    public interface IMaterialService
    {
        Task<MaterialDto> CreateMaterialAsync(string name, string code, decimal unitPrice, int stockQuantity, string unitOfMeasure);
        Task<MaterialDto?> GetMaterialByIdAsync(int id);
        Task<IEnumerable<MaterialDto>> GetAllMaterialsAsync();
        Task<MaterialDto> UpdateMaterialAsync(int id, string name, string code, decimal unitPrice, int stockQuantity, string unitOfMeasure);
        Task<bool> DeleteMaterialAsync(int id);
    }
}
