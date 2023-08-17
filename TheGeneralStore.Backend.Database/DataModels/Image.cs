using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGeneralStore.Backend.Database.DataModels
{
    [Table("images")]
    public class Image : BaseDataModel
    {
        public Guid ImageId { get; set; }

        [StringLength(10)]
        public string FileExtension { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        #region Relations
        public virtual Product Product { get; set; }
        public virtual int? ProductId { get; set; }
        #endregion
    }
}
