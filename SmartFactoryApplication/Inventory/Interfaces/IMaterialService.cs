using SmartFactoryApplication.Inventory.Models;

namespace SmartFactoryApplication.Inventory.Interfaces
{
    public interface IMaterialService
    {
        Task<MaterialModel> CreateMaterialAsync(string name, string code, decimal unitPrice, int stockQuantity, string unitOfMeasure);
        Task<MaterialModel?> GetMaterialByIdAsync(int id);
        Task<IEnumerable<MaterialModel>> GetAllMaterialsAsync();
        Task<MaterialModel> UpdateMaterialAsync(int id, string name, string code, decimal unitPrice, int stockQuantity, string unitOfMeasure);
        Task<bool> DeleteMaterialAsync(int id);
    }
}
