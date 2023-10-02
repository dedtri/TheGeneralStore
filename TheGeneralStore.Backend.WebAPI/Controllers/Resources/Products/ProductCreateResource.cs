namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Products
{
    public class ProductCreateResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        #region Relations
        public int CategoryId { get; set; }
        #endregion
    }
}
