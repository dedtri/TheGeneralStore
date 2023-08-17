using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public class CustomerRepository : BaseRepository<Customer, CustomerQuery>
    {
        public CustomerRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
