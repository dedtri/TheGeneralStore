using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGeneralStore.Backend.Database.DataModels
{
    [Table("products")]
    public class Product : BaseDataModel
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        #region Relations
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Image> Images { get; set; }  

        public virtual Category Category { get; set; }
        public virtual int? CategoryId { get; set; }
        #endregion
    }
}
