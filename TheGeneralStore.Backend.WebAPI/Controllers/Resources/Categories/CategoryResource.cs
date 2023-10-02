using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Products;

namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Categories
{
    public class CategoryResource : BaseResource
    {
        public string Name { get; set; }

        #region Relations
        public virtual ICollection<ProductResource> Products { get; set; }
        #endregion
    }
}
