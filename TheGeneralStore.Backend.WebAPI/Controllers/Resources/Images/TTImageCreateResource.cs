namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Images
{
    public class ImageCreateResource
    {
        public Guid ImageId { get; set; }
        public string FileExtension { get; set; }
        public IFormFile ImageFile { get; set; }

        #region Relations
        public int ProductId { get; set; }
        #endregion
    }
}
