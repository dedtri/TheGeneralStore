namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Orders
{
    public class OrderProductCreateResource
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
