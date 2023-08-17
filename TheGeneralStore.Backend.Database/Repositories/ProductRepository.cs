using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public class ProductRepository : BaseRepository<Product, ProductQuery>
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
