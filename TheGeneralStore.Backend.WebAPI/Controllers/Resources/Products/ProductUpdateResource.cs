namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Products
{
    public class ProductUpdateResouce : BaseResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
