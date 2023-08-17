using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGeneralStore.Backend.Database.DataModels
{
    [Table("customers")]
    public class Customer : BaseDataModel
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }

        #region Relations
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        #endregion
    }
}
