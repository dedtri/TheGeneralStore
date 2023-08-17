using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public class CategoryRepository : BaseRepository<Category, CategoryQuery>
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
