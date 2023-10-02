using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Categories;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Images;

namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Products
{
    public class ProductResource : BaseResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        #region Relations
        public ICollection<ImageResource> Images { get; set; }
        public CategoryResource Category { get; set; }
        #endregion
    }
}
