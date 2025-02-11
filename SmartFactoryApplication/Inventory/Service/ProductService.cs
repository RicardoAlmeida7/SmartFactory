using SmartFactoryApplication.Inventory.Interfaces.Services;
using SmartFactoryApplication.Inventory.Interfaces.UseCases;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Model;

namespace SmartFactoryApplication.Inventory.Service
{
    public class ProductService(IProductUseCases productUseCases) : IProductService
    {
        private readonly IProductUseCases _productUseCases = productUseCases;
      
        public async Task<Response<ProductModel>> CreateProductAsync(ProductModel model) => 
            await _productUseCases.CreateProductAsync(model);

        public async Task<Response<ProductModel>> DeleteProductAsync(int id) => 
            await _productUseCases.DeleteProductAsync(id);

        public async Task<Response<IEnumerable<ProductModel>>> GetAllProductsAsync() => 
            await _productUseCases.GetAllProductsAsync();

        public async Task<Response<ProductModel?>> GetProductByIdAsync(int id) => 
            await _productUseCases.GetProductByIdAsync(id);

        public async Task<Response<ProductModel>> UpdateProductAsync(ProductModel model) => 
            await _productUseCases.UpdateProductAsync(model);
    }
}
