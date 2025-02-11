using Microsoft.AspNetCore.Mvc;
using SmartFactoryApplication.Inventory.Interfaces.Services;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Utils;

namespace SmartFactoryApi.Controllers.Inventory
{
    [ApiController]
    [Route("inventory/[controller]/v1")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var materials = await _productService.GetAllProductsAsync();
                return StatusCode((int)materials.StatusCode, materials.Data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductModel model)
        {
            try
            {
                var response = await _productService.CreateProductAsync(model);
                object? data = response.IsValid ? response.Data : response.Errors;
                return StatusCode((int)response.StatusCode, data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProductModel model)
        {
            return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR, "Not implemented [UPDATE]");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR, "Not implemented [DELETE]");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var response = await _productService.GetProductByIdAsync(id);
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
