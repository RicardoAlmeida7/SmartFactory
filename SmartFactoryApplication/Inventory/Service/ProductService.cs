using AutoMapper;
using SmartFactoryApplication.Inventory.Interfaces;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

namespace SmartFactoryApplication.Inventory.Service
{
    public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductModel> CreateProductAsync(string name, string code, string category, decimal price, int stockQuantity)
        {
            var product = new Product(name, code, category, price, stockQuantity);
            var createdProduct = await _productRepository.CreateAsync(product);
            return _mapper.Map<ProductModel>(createdProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return false;
            return await _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public async Task<ProductModel?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductModel>(product);
        }

        public async Task<ProductModel> UpdateProductAsync(int id, string name, string code, string category, decimal price, int stockQuantity)
        {
            var product = new Product(name, code, category, price, stockQuantity) { Id = id };
            var updatedProduct = await _productRepository.UpdateAsync(product);
            return _mapper.Map<ProductModel>(updatedProduct);
        }
    }
}
