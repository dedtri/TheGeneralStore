using TheGeneralStore.Backend.WebAPI.Controllers.Resources.Products;

namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Orders
{
    public class OrderProductResource
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        #region Relations
        public int ProductId { get; set; }
        public virtual ProductResource Product { get; set; }
        #endregion
    }
}
