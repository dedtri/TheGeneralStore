using Microsoft.EntityFrameworkCore;

namespace TheGeneralStore.Backend.Database.DataModels
{
    [Index(nameof(IsDeleted), IsUnique = false)]
    public abstract class BaseDataModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
