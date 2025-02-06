using SmartFactoryApplication.Inventory.Interfaces.Services;
using SmartFactoryApplication.Inventory.Interfaces.UseCases;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Model;

namespace SmartFactoryApplication.Inventory.Service
{
    public class MaterialService(IMaterialUseCases useCases) : IMaterialService
    {
        private readonly IMaterialUseCases _useCases = useCases;

        public async Task<Response<MaterialModel>> CreateMaterialAsync(MaterialModel model) =>
            await _useCases.CreatMaterialAsync(model);

        public async Task<Response<MaterialModel>> DeleteMaterialAsync(int id) => await _useCases.DeleteMaterialAsync(id);

        public async Task<Response<IEnumerable<MaterialModel>>> GetAllMaterialsAsync() => await _useCases.GetAllMaterialsAsync();

        public async Task<Response<MaterialModel>> GetMaterialByIdAsync(int id) => await _useCases.GetMaterialByIdAsync(id);

        public Task<Response<MaterialModel>> UpdateMaterialAsync(MaterialModel model)
        {
            throw new NotImplementedException();
        }
    }
}
