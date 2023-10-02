using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheGeneralStore.Backend.Database.DataModels
{
    [Index(nameof(CreatedAt), IsUnique = false)]
    [Index(nameof(IsDeleted), IsUnique = false)]
    public abstract class BaseDataModel
    {
        public int Id { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
