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
    public class MaterialUseCases(IMaterialRepository repository, IValidationError validationError, IMapper mapper) : IMaterialUseCases
    {
        private readonly IMaterialRepository _materialRepository = repository;
        private readonly IValidationError _validationError = validationError;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<MaterialModel>> CreatMaterialAsync(MaterialModel model)
        {
            await ValidateCreateMaterialAsync(model);

            if (_validationError.HasValidationErrors())
                return Response<MaterialModel>.Fail(_validationError.GetValidationErrors());

            var createdMaterial = await _materialRepository.CreateAsync(_mapper.Map<Material>(model));
            return Response<MaterialModel>.Created(_mapper.Map<MaterialModel>(createdMaterial));
        }


        public async Task<Response<MaterialModel>> GetMaterialByIdAsync(int id)
        {
            var material = await _materialRepository.GetByIdAsync(id);
            if (material == null)
            {
                _validationError.AddError(nameof(MaterialModel.Id), ConstantMessages.MATERIAL_NOT_FOUND);
                return Response<MaterialModel>.NotFound(_validationError.GetValidationErrors());
            }

            return Response<MaterialModel>.Success(_mapper.Map<MaterialModel>(material));
        }

        public async Task<Response<MaterialModel>> DeleteMaterialAsync(int id)
        {
            var response = await GetMaterialByIdAsync(id);

            if (!response.IsValid) return response;

            var success = await _materialRepository.DeleteAsync(_mapper.Map<Material>(response.Data));

            if (!success)
            {
                _validationError.AddError(nameof(MaterialModel.Id), $"{ConstantMessages.FAILED_DELETE_MATERIAL} :{response.Data?.Code}.");
                return Response<MaterialModel>.Fail(_validationError.GetValidationErrors());
            }

            return Response<MaterialModel>.NoContent();
        }

        public async Task<Response<MaterialModel>> UpdateMaterialAsync(int id, MaterialModel model)
        {
            var existingMaterial = await _materialRepository.GetByIdAsync(id);
            if (existingMaterial == null)
            {
                _validationError.AddError(nameof(MaterialModel.Id), ConstantMessages.MATERIAL_NOT_FOUND);
                return Response<MaterialModel>.NotFound(_validationError.GetValidationErrors());
            }

            ValidateMaterialData(model);

            if (!string.IsNullOrWhiteSpace(model.Code) && model.Code != existingMaterial.Code)
            {
                var exists = await _materialRepository.ExistsByCodeAsync(model.Code);
                if (exists)
                    _validationError.AddError(nameof(model.Code), ConstantMessages.DUPLICATE_MATERIAL_CODE);
            }

            if (_validationError.HasValidationErrors())
                return Response<MaterialModel>.Fail(_validationError.GetValidationErrors());

            _mapper.Map(model, existingMaterial);
            var updatedMaterial = await _materialRepository.UpdateAsync(existingMaterial);

            return Response<MaterialModel>.Success(_mapper.Map<MaterialModel>(updatedMaterial));
        }

        public async Task<Response<IEnumerable<MaterialModel>>> GetAllMaterialsAsync()
        {
            var materials = await _materialRepository.GetAllAsync();
            var list = _mapper.Map<IEnumerable<MaterialModel>>(materials);
            return Response<IEnumerable<MaterialModel>>.Success(list);
        }

        private async Task ValidateCreateMaterialAsync(MaterialModel model)
        {
            ValidateMaterialData(model);

            if (!string.IsNullOrWhiteSpace(model.Name) && await _materialRepository.ExistsByNameAsync(model.Name))
                _validationError.AddError(nameof(model.Name), ConstantMessages.DUPLICATE_MATERIAL_NAME);

            if (!string.IsNullOrWhiteSpace(model.Code) && await _materialRepository.ExistsByCodeAsync(model.Code))
                _validationError.AddError(nameof(model.Code), ConstantMessages.DUPLICATE_MATERIAL_CODE);
        }

        private void ValidateMaterialData(MaterialModel model)
        {
            AddValidationError(string.IsNullOrWhiteSpace(model.Name), nameof(model.Name), ConstantMessages.REQUIRED_MATERIAL_NAME);
            AddValidationError(string.IsNullOrWhiteSpace(model.Code), nameof(model.Code), ConstantMessages.REQUIRED_MATERIAL_CODE);
            AddValidationError(string.IsNullOrWhiteSpace(model.UnitOfMeasure), nameof(model.UnitOfMeasure), ConstantMessages.REQUIRED_UNIT_OF_MESUARE);
            AddValidationError(model.StockQuantity < 0, nameof(model.StockQuantity), ConstantMessages.QUANTITY_STOCK_CANNOT_BE_NEGATIVE);
            AddValidationError(model.UnitPrice < 0, nameof(model.UnitPrice), ConstantMessages.UNIT_PRICE_CANNOT_BE_NEGATIVE);
        }

        private void AddValidationError(bool condition, string field, string message)
        {
            if (condition)
                _validationError.AddError(field, message);
        }
    }
}
