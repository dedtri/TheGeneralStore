using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Images;

namespace TheGeneralStore.Backend.WebAPI.Persistence.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment environment;

        public ImageService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        #region SaveImage
        public Tuple<int, string> SaveImage(ImageCreateResource image)
        {
            try
            {
                var contentPath = this.environment.ContentRootPath;
                var folderName = "Uploads";

                var path = Path.Combine(contentPath, folderName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var ext = Path.GetExtension(image.ImageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg", ".avif" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                image.ImageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, uniqueString);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        }
        #endregion

        #region DeleteImage
        public bool DeleteImage(Image image)
        {
            try
            {
                var folderPath = "Uploads\\";

                var contentPath = this.environment.ContentRootPath;
                var path = Path.Combine(contentPath, folderPath, image.ImageId + image.FileExtension);
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}