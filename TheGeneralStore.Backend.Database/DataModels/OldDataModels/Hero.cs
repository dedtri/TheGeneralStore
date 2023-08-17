namespace TheGeneralStore.Backend.Database.DataModels.OldDataModels
{
    public class Hero : BaseDataModel
    {
        public int Name { get; set; }

        #region Relations

        public Team Team { get; set; }

        public int TeamId { get; set; }

        #endregion
    }
}
