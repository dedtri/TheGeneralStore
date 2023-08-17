using AutoMapper;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Products;

namespace TheGeneralStore.Backend.WebAPI.Core.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            // API to Resource
            CreateMap<Product, ProductResource>();

            // Resource to API
            CreateMap<ProductCreateResource, Product>();
        }
    }
}
