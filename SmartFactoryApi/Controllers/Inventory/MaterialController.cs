using Microsoft.AspNetCore.Mvc;
using SmartFactoryApplication.Inventory.Interfaces.Services;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Utils;

namespace SmartFactoryApi.Controllers.Inventory
{
    [ApiController]
    [Route("inventory/[controller]/v1")]
    public class MaterialController(IMaterialService materialService) : ControllerBase
    {
        private readonly IMaterialService _materialService = materialService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var materials = await _materialService.GetAllMaterialsAsync();
                return StatusCode((int)materials.StatusCode, materials.Data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MaterialModel model)
        {
            try
            {
                var response = await _materialService.CreateMaterialAsync(model);
                object? data = response.IsValid ? response.Data : response.Errors;
                return StatusCode((int)response.StatusCode, data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, MaterialModel model)
        {
            try
            {
                var response = await _materialService.UpdateMaterialAsync(id, model);
                object? data = response.IsValid ? response.Data : response.Errors;
                return StatusCode((int)response.StatusCode, data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var response = await _materialService.DeleteMaterialAsync(id);
                object? data = response.IsValid ? response.Data : response.Errors;
                return StatusCode((int)response.StatusCode, data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var response = await _materialService.GetMaterialByIdAsync(id);
                object? data = response.IsValid ? response.Data : response.Errors;
                return StatusCode((int)response.StatusCode, data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR);
            }

        }
    }
}
