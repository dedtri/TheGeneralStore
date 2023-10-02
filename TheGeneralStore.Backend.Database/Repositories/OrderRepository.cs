using Microsoft.EntityFrameworkCore;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;
using static System.Net.Mime.MediaTypeNames;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public class OrderRepository : BaseRepository<Order, OrderQuery>
    {
        public OrderRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Order> ApplyRelations(IQueryable<Order> query)
        {
            query = query
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product);

            return base.ApplyRelations(query);
        }
        #endregion

        #region ApplyFiltering
        protected override IQueryable<Order> ApplyFiltering(IQueryable<Order> query, OrderQuery queryModel)
        {
            if (queryModel.CustomerId.HasValue)
                query = query.Where(x => x.CustomerId == queryModel.CustomerId.Value);

            return base.ApplyFiltering(query, queryModel);
        }
        #endregion
    }
}
