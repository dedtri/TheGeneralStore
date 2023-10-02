using AutoMapper;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Products;

namespace TheGeneralStore.Backend.WebAPI.Core.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            // Mapping DataModel to Resource
            CreateMap<Product, ProductResource>();

            // Mapping Resource to DataModel
            CreateMap<ProductCreateResource, Product>();
            CreateMap<ProductUpdateResouce, Product>();
        }
    }
}
