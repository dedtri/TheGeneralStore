namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Orders
{
    public class OrderResource : BaseResource
    {
        public DateTime Date { get; set; }
        public decimal Total_Price { get; set; }

        #region Relations
        public virtual ICollection<OrderProductResource> OrderProducts { get; set; }

        public int CustomerId { get; set; }
        #endregion
    }
}
