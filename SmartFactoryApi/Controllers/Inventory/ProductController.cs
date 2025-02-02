using Microsoft.AspNetCore.Mvc;
using SmartFactoryApplication.Inventory.Interfaces;
using SmartFactoryApplication.Inventory.Models;

namespace SmartFactoryApi.Controllers.Inventory
{
    [ApiController]
    [Route("inventory/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IEnumerable<ProductModel>> Get()
        {
            return await _productService.GetAllProductsAsync();
        }

        [HttpPost]
        public async Task<ProductModel> Create(ProductModel model)
        {
            return await _productService.CreateProductAsync(model.Name, model.Code, model.Category, model.Price, model.StockQuantity);
        }

        [HttpPut]
        public async Task<ProductModel> Update(ProductModel model)
        {
            return await _productService.UpdateProductAsync(model.Id, model.Name, model.Code, model.Category, model.Price, model.StockQuantity);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _productService.DeleteProductAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<ProductModel?> Get(int id)
        {
            return await _productService.GetProductByIdAsync(id);
        }
    }
}
