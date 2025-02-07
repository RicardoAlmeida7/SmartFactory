using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Model;

namespace SmartFactoryApplication.Inventory.Interfaces.Services
{
    public interface IMaterialService
    {
        Task<Response<MaterialModel>> CreateMaterialAsync(MaterialModel model);
        Task<Response<MaterialModel>> GetMaterialByIdAsync(int id);
        Task<Response<IEnumerable<MaterialModel>>> GetAllMaterialsAsync();
        Task<Response<MaterialModel>> UpdateMaterialAsync(int id, MaterialModel model);
        Task<Response<MaterialModel>> DeleteMaterialAsync(int id);
    }
}
