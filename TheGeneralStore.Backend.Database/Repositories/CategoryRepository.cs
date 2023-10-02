using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public class CategoryRepository : BaseRepository<Category, CategoryQuery>
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {

        }

        #region ApplyRelations
        protected override IQueryable<Category> ApplyRelations(IQueryable<Category> query)
        {
            query = query
                .Include(x => x.Products)
                .ThenInclude(x => x.Images);

            return base.ApplyRelations(query);
        }
        #endregion

        #region GetByNameAsync
        public virtual async Task<Category> GetByNameAsync(string name, bool includeRelated = false)
        {
            if (!includeRelated)
                return await context.Categories.SingleOrDefaultAsync(x => x.Name == name);

            var query = context.Categories.AsQueryable();

            query = this.ApplyRelations(query);

            return await query.SingleOrDefaultAsync(x => x.Name == name);
        }
        #endregion
    }
}
