using AutoMapper;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Images;

namespace TheGeneralStore.Backend.WebAPI.Core.Mappings
{
    public class ImageMappings : Profile
    {
        public ImageMappings()
        {
            // API to Resource
            CreateMap<Image, ImageResource>();

            // Resource to API
            CreateMap<ImageCreateResource, Image>();
        }
    }
}
