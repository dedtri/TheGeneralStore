using AutoMapper;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Categories;

namespace TheGeneralStore.Backend.WebAPI.Core.Mappings
{
    public class CategoryMappings : Profile
    {
        public CategoryMappings()
        {
            // API to Resource
            CreateMap<Category, CategoryResource>();

            // Resource to API
            CreateMap<CategoryCreateResource, Category>();
        }
    }
}
