using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGeneralStore.Backend.Database.DataModels
{
    [Table("carts")]
    public class Cart : BaseDataModel
    {
        public int Quantity { get; set; }

        #region Relations
        public virtual Customer Customer { get; set; }
        public int? CustomerId { get; set; }

        public virtual Product Product { get; set; }
        public int? ProductId { get; set; }
        #endregion
    }
}
