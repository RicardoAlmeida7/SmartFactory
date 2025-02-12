using Microsoft.AspNetCore.Mvc;
using SmartFactoryApplication.Inventory.Interfaces.Services;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Utils;

namespace SmartFactoryApi.Controllers.Inventory
{
    [ApiController]
    [Route("inventory/v1/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var materials = await _productService.GetAllProductsAsync();
                return StatusCode((int)materials.StatusCode, materials.Data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR, "Error: [GET]");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductModel model)
        {
            try
            {
                var response = await _productService.CreateProductAsync(model);
                object? data = response.IsValid ? response.Data : response.Errors;
                return StatusCode((int)response.StatusCode, data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR, "Error: [POST]");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductModel model)
        {
            try
            {
                var response = await _productService.UpdateProductAsync(model);
                if (!response.IsValid)
                    return StatusCode((int)response.StatusCode, response.Errors);

                return StatusCode((int)response.StatusCode, response.Data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR, "Error: [PUT]");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(id);
                if (!response.IsValid)
                    return StatusCode((int)response.StatusCode, response.Errors);

                return StatusCode((int)response.StatusCode, response.Data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR, "Error: [DELETE]");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            try
            {
                var response = await _productService.GetProductByIdAsync(id);
                object? data = response.IsValid ? response.Data : response.Errors;
                return StatusCode((int)response.StatusCode, data);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCodes.INTERNAL_SERVER_ERROR, "Error: [GET]");
            }
        }
    }
}
