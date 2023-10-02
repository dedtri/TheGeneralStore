using AutoMapper;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Categories;

namespace TheGeneralStore.Backend.WebAPI.Core.Mappings
{
    public class CategoryMappings : Profile
    {
        public CategoryMappings()
        {
            // Mapping DataModel to Resource
            CreateMap<Category, CategoryResource>();

            // Mapping Resource to DataModel
            CreateMap<CategoryCreateResource, Category>();
            CreateMap<CategoryUpdateResource, Category>();

        }
    }
}
