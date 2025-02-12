using AutoMapper;
using SmartFactoryApplication.Inventory.Interfaces.UseCases;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Model;
using SmartFactoryApplication.Utils;
using SmartFactoryApplication.Validation;
using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

namespace SmartFactoryApplication.Inventory.UseCases
{
    public class ProductUseCases(
        IProductRepository productRepository, 
        IMaterialRepository materialRepository, 
        IMapper mapper,
        IValidationError validationError) : IProductUseCases
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMaterialRepository _materialRepository = materialRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidationError _validationError = validationError;

        public async Task<Response<ProductModel>> CreateProductAsync(ProductModel model)
        {
            await ValidateProductDataAsync(model);

            if (_validationError.HasValidationErrors())
                return Response<ProductModel>.Fail(_validationError.GetValidationErrors());

            var createdProduct = await _productRepository.CreateAsync(_mapper.Map<Product>(model));
            return Response<ProductModel>.Created(_mapper.Map<ProductModel>(createdProduct));
        }

        public async Task<Response<ProductModel>> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                _validationError.AddError(nameof(ProductModel.Id), ConstantMessages.MATERIAL_NOT_FOUND);
                return Response<ProductModel>.NotFound(_validationError.GetValidationErrors());
            }

            return Response<ProductModel>.NoContent();
        }

        public async Task<Response<IEnumerable<ProductModel>>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var list = _mapper.Map<IEnumerable<ProductModel>>(products);
            return Response<IEnumerable<ProductModel>>.Success(list);
        }

        public async Task<Response<ProductModel?>> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                _validationError.AddError(nameof(ProductModel.Id), ConstantMessages.MATERIAL_NOT_FOUND);
                return Response<ProductModel?>.NotFound(_validationError.GetValidationErrors());
            }

            return Response<ProductModel?>.Success(_mapper.Map<ProductModel>(product));
        }

        public async Task<Response<ProductModel>> UpdateProductAsync(ProductModel model)
        {
            var idNotNull = (int)(model.Id != null ? model.Id : 0);

            var existingProduct = await _productRepository.GetByIdAsync(idNotNull);
            
            if (existingProduct == null)
            {
                _validationError.AddError(nameof(ProductModel.Id), ConstantMessages.MATERIAL_NOT_FOUND);
                return Response<ProductModel>.NotFound(_validationError.GetValidationErrors());
            }

            ValidateProductModel(model);

            if (_validationError.HasValidationErrors())
                return Response<ProductModel>.Fail(_validationError.GetValidationErrors());

            if (!string.IsNullOrWhiteSpace(model.Code) && model.Code != existingProduct.Code)
            {
                var exists = await _productRepository.ExistsByCodeAsync(model.Code);
                if (exists)
                    _validationError.AddError(nameof(model.Code), ConstantMessages.DUPLICATE_PRODUCT_CODE);
            }

            if (!string.IsNullOrWhiteSpace(model.Name) && model.Name != existingProduct.Name)
            {
                var exists = await _productRepository.ExistsByNameAsync(model.Name);
                if (exists)
                    _validationError.AddError(nameof(model.Name), ConstantMessages.DUPLICATE_PRODUCT_NAME);
            }

            _mapper.Map(model, existingProduct);
            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);

            return Response<ProductModel>.Success(_mapper.Map<ProductModel>(updatedProduct));
        }

        private async Task ValidateProductDataAsync(ProductModel model)
        {
            ValidateProductModel(model);

            if (!string.IsNullOrWhiteSpace(model.Name) && await _productRepository.ExistsByNameAsync(model.Name))
                _validationError.AddError(nameof(model.Name), ConstantMessages.DUPLICATE_PRODUCT_NAME);

            if (!string.IsNullOrWhiteSpace(model.Code) && await _productRepository.ExistsByCodeAsync(model.Code))
                _validationError.AddError(nameof(model.Code), ConstantMessages.DUPLICATE_PRODUCT_CODE);

            await ValidateMaterialList(model);
        }

        private void ValidateProductModel(ProductModel model)
        {
            _validationError.AddValidationError(string.IsNullOrEmpty(model.Name), nameof(model.Name), ConstantMessages.REQUIRED_PRODUCT_NAME);
            _validationError.AddValidationError(string.IsNullOrEmpty(model.Code), nameof(model.Code), ConstantMessages.REQUIRED_PRODUCT_CODE);
            _validationError.AddValidationError(string.IsNullOrEmpty(model.Category), nameof(model.Category), ConstantMessages.REQUIRED_PRODUCT_CATEGORY);
            _validationError.AddValidationError(model.StockQuantity < 0, nameof(model.StockQuantity), ConstantMessages.QUANTITY_STOCK_CANNOT_BE_NEGATIVE);
            _validationError.AddValidationError(model.Price < 0, nameof(model.Price), ConstantMessages.UNIT_PRICE_CANNOT_BE_NEGATIVE);
        }

        private async Task ValidateMaterialList(ProductModel model)
        {
            foreach(var materialId in model.ProductMaterials.Select(pm => pm.MaterialId).Where(id => id != 0))
            {
                var exist = await _materialRepository.GetByIdAsync(materialId);
                if (exist is null)
                    _validationError.AddError($"{ConstantMessages.MATERIAL_NOT_FOUND}", $"ID: [{materialId}]");
            }
        }
    }
}
