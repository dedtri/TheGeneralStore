using AutoMapper;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Images;

namespace TheGeneralStore.Backend.WebAPI.Core.Mappings
{
    public class ImageMappings : Profile
    {
        public ImageMappings()
        {
            // Mapping DataModel to Resource
            CreateMap<Image, ImageResource>();

            // Mapping Resource to DataModel
            CreateMap<ImageCreateResource, Image>();
        }
    }
}
