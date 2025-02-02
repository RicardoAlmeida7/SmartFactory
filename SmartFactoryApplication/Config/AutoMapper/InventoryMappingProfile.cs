using AutoMapper;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryDomain.Entities.Inventory;

namespace SmartFactoryApplication.Config.AutoMapper
{
    public class InventoryMappingProfile : Profile
    {
        public InventoryMappingProfile()
        {
            CreateMap<Product, ProductModel>()
                 .ForMember(dest => dest.ProductMaterials, opt => opt.MapFrom(src => src.ProductMaterials))
                 .ReverseMap();

            CreateMap<MaterialModel, Material>()
                .ReverseMap();

            CreateMap<ProductMaterial, ProductMaterialModel>()
                .ReverseMap();

            CreateMap<StockMovementBase, StockMovementModel>()
                .ReverseMap();
        }
    }
}
