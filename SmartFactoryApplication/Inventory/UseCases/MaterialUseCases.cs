using AutoMapper;
using SmartFactoryApplication.Inventory.Interfaces.UseCases;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Model;
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
            return Response<MaterialModel>.Success(_mapper.Map<MaterialModel>(createdMaterial));
        }


        public async Task<Response<MaterialModel>> GetMaterialByIdAsync(int id)
        {
            var material = await _materialRepository.GetByIdAsync(id);
            if (material == null)
            {
                _validationError.AddError(nameof(MaterialModel.Id), "Material não encontrado.");
                return Response<MaterialModel>.Fail(_validationError.GetValidationErrors());
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
                _validationError.AddError(nameof(MaterialModel.Id), $"Falha ao excluir material: {response.Data?.Code}.");
                return Response<MaterialModel>.Fail(_validationError.GetValidationErrors());
            }

            var successMessages = new Dictionary<string, string>
            {
                { "Success", "Material excluído com sucesso!" }
            };

            return Response<MaterialModel>.Success(successMessages);
        }

        public async Task<Response<MaterialModel>> UpdateMaterialAsync(int id, MaterialModel model)
        {
            var existingMaterial = await _materialRepository.GetByIdAsync(id);
            if (existingMaterial == null)
            {
                _validationError.AddError(nameof(MaterialModel.Id), "Material não encontrado.");
                return Response<MaterialModel>.Fail(_validationError.GetValidationErrors());
            }

            ValidateMaterialData(model);

            if (!string.IsNullOrWhiteSpace(model.Code) && model.Code != existingMaterial.Code)
            {
                if (await _materialRepository.ExistsByCodeAsync(model.Code))
                {
                    _validationError.AddError(nameof(model.Code), "Já existe um material com esse código.");
                }
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
                _validationError.AddError(nameof(model.Name), "Já existe um material com esse nome.");

            if (!string.IsNullOrWhiteSpace(model.Code) && await _materialRepository.ExistsByCodeAsync(model.Code))
                _validationError.AddError(nameof(model.Code), "Já existe um material com esse código.");
        }

        private void ValidateMaterialData(MaterialModel model)
        {
            AddValidationError(string.IsNullOrWhiteSpace(model.Name), nameof(model.Name), "O nome do material é obrigatório.");
            AddValidationError(string.IsNullOrWhiteSpace(model.Code), nameof(model.Code), "O código do material é obrigatório.");
            AddValidationError(string.IsNullOrWhiteSpace(model.UnitOfMeasure), nameof(model.UnitOfMeasure), "A unidade de medida é obrigatória.");
            AddValidationError(model.StockQuantity < 0, nameof(model.StockQuantity), "A quantidade em estoque não pode ser negativa.");
            AddValidationError(model.UnitPrice < 0, nameof(model.UnitPrice), "O preço unitário não pode ser negativo.");
        }

        private void AddValidationError(bool condition, string field, string message)
        {
            if (condition)
                _validationError.AddError(field, message);
        }
    }
}
