using Microsoft.EntityFrameworkCore;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public class ProductRepository : BaseRepository<Product, ProductQuery>
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }


        #region ApplyRelations
        protected override IQueryable<Product> ApplyRelations(IQueryable<Product> query)
        {
            query = query
                .Include(x => x.Images)
                .Include(x => x.Category);

            return base.ApplyRelations(query);
        }
        #endregion
    }
}
