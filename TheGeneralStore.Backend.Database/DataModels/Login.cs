using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGeneralStore.Backend.Database.DataModels
{
    [Table("logins")]
    public class Login : BaseDataModel
    {
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(20)]
        public string Role { get; set; }
    }
}
