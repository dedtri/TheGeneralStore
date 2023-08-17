namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Images
{
    public class ImageResource : BaseResource
    {
        public Guid ImageId { get; set; }
        public string FileExtension { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
