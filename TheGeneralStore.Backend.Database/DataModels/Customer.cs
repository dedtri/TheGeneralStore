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
        [StringLength(40)]
        public string? Role { get; set; }
        [StringLength(100)]
        public string? Password { get; set; }
        [StringLength(255)]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }


        #region Relations
        public virtual ICollection<Order> Orders { get; set; }
        #endregion
    }
}
