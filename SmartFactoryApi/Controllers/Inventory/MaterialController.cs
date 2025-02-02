using Microsoft.AspNetCore.Mvc;
using SmartFactoryApplication.Inventory.Interfaces;
using SmartFactoryApplication.Inventory.Models;

namespace SmartFactoryApi.Controllers.Inventory
{
    [ApiController]
    [Route("inventory/[controller]")]
    public class MaterialController(IMaterialService materialService) : ControllerBase
    {
        private readonly IMaterialService _materialService = materialService;

        [HttpGet]
        public async Task<IEnumerable<MaterialModel>> Get()
        {
            return await _materialService.GetAllMaterialsAsync();
        }

        [HttpPost]
        public async Task<MaterialModel> Create(MaterialModel model)
        {
            return await _materialService.CreateMaterialAsync(model.Name, model.Code, model.UnitPrice, model.StockQuantity, model.UnitOfMeasure);
        }

        [HttpPut]
        public async Task<MaterialModel> Update(MaterialModel model)
        {
            return await _materialService.UpdateMaterialAsync(model.Id, model.Name, model.Code, model.UnitPrice, model.StockQuantity, model.UnitOfMeasure);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _materialService.DeleteMaterialAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<MaterialModel?> Get(int id)
        {
            return await _materialService.GetMaterialByIdAsync(id);
        }
    }
}
