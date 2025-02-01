using AutoMapper;
using SmartFactoryApplication.Inventory.Models;
using SmartFactoryDomain.Entities.Inventory;

namespace SmartFactoryApplication.Config.AutoMapper
{
    public class InvantoryMappingProfile : Profile
    {
        public InvantoryMappingProfile()
        {
            CreateMap<Product, ProductDto>()
              .ReverseMap();

            CreateMap<Material, MaterialDto>()
                .ReverseMap();

            CreateMap<ProductMaterial, ProductMaterialDto>()
                .ReverseMap();

            CreateMap<StockMovement, StockMovementDto>()
                .ReverseMap();
        }
    }
}
