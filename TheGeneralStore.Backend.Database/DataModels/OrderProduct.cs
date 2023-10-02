using System.ComponentModel.DataAnnotations.Schema;

namespace TheGeneralStore.Backend.Database.DataModels
{
    [Table("orderproducts")]
    public class OrderProduct : BaseDataModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        #region Relations
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; } 
        #endregion
    }
}
