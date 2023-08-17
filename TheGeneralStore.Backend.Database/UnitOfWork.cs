namespace TheGeneralStore.Backend.Database
{
    public class UnitOfWork
    {
        private readonly DatabaseContext databaseContext;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        #region SaveChangesAsync
        public async Task SaveChangesAsync()
        {
            await this.databaseContext.SaveChangesAsync();
        }
        #endregion
    }
}
