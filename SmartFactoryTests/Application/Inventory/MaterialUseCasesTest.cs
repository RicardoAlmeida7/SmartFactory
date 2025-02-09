using AutoMapper;
using FluentAssertions;
using Moq;
using SmartFactoryApplication.Inventory.Interfaces.UseCases;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Inventory.UseCases;
using SmartFactoryApplication.Utils;
using SmartFactoryApplication.Validation;
using SmartFactoryDomain.Entities.Inventory;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

namespace SmartFactoryTests.Application.Inventory
{
    public class MaterialUseCasesTest
    {
        private readonly Mock<IMaterialRepository> _materialRepositoryMock;
        private readonly Mock<IValidationError> _validationErrorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IMaterialUseCases _materialUseCases;

        public MaterialUseCasesTest()
        {
            _materialRepositoryMock = new Mock<IMaterialRepository>();
            _validationErrorMock = new Mock<IValidationError>();
            _mapperMock = new Mock<IMapper>();

            _materialUseCases = new MaterialUseCases(
                _materialRepositoryMock.Object,
                _validationErrorMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task CreateMaterialAsync_ShouldReturnCreatedResponse_WhenMaterialIsValid()
        {
            // Arrange
            var materialModel = new MaterialModel(id: 0, name: "Material1", code: "M001", unitPrice: 10, stockQuantity: 100, unitOfMeasure: "unit");
            var materialEntity = new Material(name: "Material1", code: "M001", unitPrice: 10, stockQuantity: 100, unitOfMeasure: "unit");

            _mapperMock.Setup(m => m.Map<Material>(materialModel)).Returns(materialEntity);
            _mapperMock.Setup(m => m.Map<MaterialModel>(materialEntity)).Returns(materialModel);
            _materialRepositoryMock.Setup(r => r.CreateAsync(materialEntity)).ReturnsAsync(materialEntity);
            _validationErrorMock.Setup(v => v.HasValidationErrors()).Returns(false);

            // Act
            var result = await _materialUseCases.CreatMaterialAsync(materialModel);

            // Assert
            result.IsValid.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCodes.CREATED);
            result.Data.Should().BeEquivalentTo(materialModel);
            _materialRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Material>()), Times.Once);
        }

        [Fact]
        public async Task GetMaterialByIdAsync_ShouldReturnSuccessResponse_WhenMaterialExists()
        {
            // Arrange
            int materialId = 1;
            var materialEntity = new Material(name: "Material1", code: "M001", unitPrice: 10, stockQuantity: 100, unitOfMeasure: "unit");
            var materialModel = new MaterialModel(id: materialId, name: "Material1", code: "M001", unitPrice: 10, stockQuantity: 100, unitOfMeasure: "unit");

            _materialRepositoryMock.Setup(r => r.GetByIdAsync(materialId)).ReturnsAsync(materialEntity);
            _mapperMock.Setup(m => m.Map<MaterialModel>(materialEntity)).Returns(materialModel);

            // Act
            var result = await _materialUseCases.GetMaterialByIdAsync(materialId);

            // Assert
            result.IsValid.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCodes.OK);
            result.Data.Should().BeEquivalentTo(materialModel);
        }

        [Fact]
        public async Task GetMaterialByIdAsync_ShouldReturnNotFoundResponse_WhenMaterialDoesNotExist()
        {
            // Arrange
            int materialId = 1;
            _materialRepositoryMock.Setup(r => r.GetByIdAsync(materialId)).ReturnsAsync((Material)null);
            _validationErrorMock.Setup(v => v.GetValidationErrors()).Returns(new Dictionary<string, string> { { "Id", ConstantMessages.MATERIAL_NOT_FOUND } });

            // Act
            var result = await _materialUseCases.GetMaterialByIdAsync(materialId);

            // Assert
            result.IsValid.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCodes.NOT_FOUND);
            result.Errors.Should().ContainKey("Id");
        }

        [Fact]
        public async Task DeleteMaterialAsync_ShouldReturnNoContentResponse_WhenDeletionSucceeds()
        {
            // Arrange
            int materialId = 1;
            var materialEntity = new Material(name: "Material1", code: "M001", unitPrice: 10, stockQuantity: 100, unitOfMeasure: "unit");
            var materialModel = new MaterialModel(id: materialId, name: "Material1", code: "M001", unitPrice: 10, stockQuantity: 100, unitOfMeasure: "unit");

            _materialRepositoryMock.Setup(r => r.GetByIdAsync(materialId)).ReturnsAsync(materialEntity);
            _mapperMock.Setup(m => m.Map<MaterialModel>(materialEntity)).Returns(materialModel);
            _mapperMock.Setup(m => m.Map<Material>(materialModel)).Returns(materialEntity);
            _materialRepositoryMock.Setup(r => r.DeleteAsync(materialEntity)).ReturnsAsync(true);

            // Act
            var result = await _materialUseCases.DeleteMaterialAsync(materialId);

            // Assert
            result.IsValid.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCodes.NO_CONTENT);
        }

        [Fact]
        public async Task DeleteMaterialAsync_ShouldReturnFailResponse_WhenDeletionFails()
        {
            // Arrange
            int materialId = 1;
            var materialEntity = new Material(name: "Material1", code: "M001", unitPrice: 10, stockQuantity: 100, unitOfMeasure: "unit");
            var materialModel = new MaterialModel(id: materialId, name: "Material1", code: "M001", unitPrice: 10, stockQuantity: 100, unitOfMeasure: "unit");

            _materialRepositoryMock.Setup(r => r.GetByIdAsync(materialId)).ReturnsAsync(materialEntity);
            _mapperMock.Setup(m => m.Map<MaterialModel>(materialEntity)).Returns(materialModel);
            _mapperMock.Setup(m => m.Map<Material>(materialModel)).Returns(materialEntity);
            _materialRepositoryMock.Setup(r => r.DeleteAsync(materialEntity)).ReturnsAsync(false);
            _validationErrorMock.Setup(v => v.GetValidationErrors()).Returns(new Dictionary<string, string> { { "Id", $"{ConstantMessages.FAILED_DELETE_MATERIAL} :{materialModel.Code}." } });

            // Act
            var result = await _materialUseCases.DeleteMaterialAsync(materialId);

            // Assert
            result.IsValid.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCodes.BAD_REQUEST);
            result.Errors.Should().ContainKey("Id");
        }

        [Fact]
        public async Task GetAllMaterialsAsync_ShouldReturnSuccessResponse_WhenMaterialsExist()
        {
            // Arrange
            var materials = new List<Material>
            {
                new() { Id = 1, Name = "Material1", Code = "M001" },
                new() { Id = 2, Name = "Material2", Code = "M002" }
            };

            var materialModels = new List<MaterialModel>
            {
                new() { Id = 1, Name = "Material1", Code = "M001" },
                new() { Id = 2, Name = "Material2", Code = "M002" }
            };

            _materialRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(materials);
            _mapperMock.Setup(m => m.Map<IEnumerable<MaterialModel>>(materials)).Returns(materialModels);

            // Act
            var result = await _materialUseCases.GetAllMaterialsAsync();

            // Assert
            result.IsValid.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCodes.OK);
            result.Data.Should().BeEquivalentTo(materialModels);
        }
    }
}
