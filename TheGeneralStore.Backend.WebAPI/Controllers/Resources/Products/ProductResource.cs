namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Products
{
    public class ProductResource : BaseResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
