using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGeneralStore.Backend.Database.DataModels
{
    [Table("Categories")]
    public class Category : BaseDataModel
    {
        [StringLength(50)]
        public string Name { get; set; }

        #region Relations
        public virtual ICollection<Product> Products { get; set; }
        #endregion
    }
}
