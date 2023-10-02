using System.ComponentModel.DataAnnotations.Schema;

namespace TheGeneralStore.Backend.Database.DataModels
{
    [Table("orders")]
    public class Order : BaseDataModel
    {
        public DateTime Date { get; set; }
        public decimal Total_Price { get; set; }

        #region Relations
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
        #endregion
    }
}
