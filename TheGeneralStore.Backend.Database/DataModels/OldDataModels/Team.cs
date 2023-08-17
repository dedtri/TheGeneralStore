using System.ComponentModel.DataAnnotations.Schema;

namespace TheGeneralStore.Backend.Database.DataModels.OldDataModels
{
    [Table("teams")]
    public class Team : BaseDataModel
    {
        public string Name { get; set; }

        #region Relations
        public ICollection<Hero> Heroes { get; set; }

        #endregion
    }
}
