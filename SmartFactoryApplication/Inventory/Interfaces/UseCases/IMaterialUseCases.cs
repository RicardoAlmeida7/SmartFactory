using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Model;

namespace SmartFactoryApplication.Inventory.Interfaces.UseCases
{
    public interface IMaterialUseCases
    {
        Task<Response<MaterialModel>> CreatMaterialAsync(MaterialModel model);
        Task<Response<MaterialModel>> DeleteMaterialAsync(int id);
        Task<Response<MaterialModel>> GetMaterialByIdAsync(int id);
        Task<Response<IEnumerable<MaterialModel>>> GetAllMaterialsAsync();
        Task<Response<MaterialModel>> UpdateMaterialAsync(int id, MaterialModel model);
    }
}
