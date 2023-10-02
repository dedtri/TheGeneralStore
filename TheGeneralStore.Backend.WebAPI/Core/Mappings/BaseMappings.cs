using AutoMapper;
using TheGeneralStore.Backend.Database.QueryModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources;

namespace TheGeneralStore.Backend.WebAPI.Core.Mappings
{
    public class BaseMappings : Profile
    {
        public BaseMappings()
        {
            // Mapping DataModel to Resource
            CreateMap(typeof(BaseQueryResult<>), typeof(BaseQueryResultResource<>));
        }
    }
}
