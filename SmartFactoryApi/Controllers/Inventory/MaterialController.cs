using Microsoft.AspNetCore.Mvc;
using SmartFactoryApplication.Inventory.Interfaces.Services;
using SmartFactoryApplication.Inventory.Models;

namespace SmartFactoryApi.Controllers.Inventory
{
    [ApiController]
    [Route("inventory/[controller]")]
    public class MaterialController(IMaterialService materialService) : ControllerBase
    {
        private readonly IMaterialService _materialService = materialService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var materials = await _materialService.GetAllMaterialsAsync();
            return Ok(materials.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MaterialModel model)
        {
            var materialCreated = await _materialService.CreateMaterialAsync(model);

            if (!materialCreated.IsValid)
            {
                return BadRequest(materialCreated.Errors);
            }

            return Ok(materialCreated.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, MaterialModel model)
        {
            var updatedMaterial = await _materialService.UpdateMaterialAsync(id, model);

            if (!updatedMaterial.IsValid)
            {
                return BadRequest(updatedMaterial.Errors);
            }

            return Ok(updatedMaterial.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var exclusion = await _materialService.DeleteMaterialAsync(id);

            if (!exclusion.IsValid)
            {
                return BadRequest(exclusion.Errors);
            }

            return Ok(exclusion.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);

            if (!material.IsValid)
            {
                return BadRequest(material.Errors);
            }

            return Ok(material.Data);
        }
    }
}
