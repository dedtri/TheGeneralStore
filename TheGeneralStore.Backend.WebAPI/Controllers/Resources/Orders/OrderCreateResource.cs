using TheGeneralStore.Backend.Database.DataModels;

namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources.Orders
{
    public class OrderCreateResource
    {
        public DateTime Date { get; set; }
        public decimal Total_Price { get; set; }

        public virtual ICollection<OrderProductCreateResource> OrderProducts { get; set; }

        public int CustomerId { get; set; }
    }
}
