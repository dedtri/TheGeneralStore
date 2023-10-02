using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheGeneralStore.Backend.Database.Core.Extensions;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public abstract class BaseRepository<T, Q> : IBaseRepository<T, Q>
           where T : BaseDataModel, new()
           where Q : BaseQueryModel
    {
        protected readonly DatabaseContext context;

        public BaseRepository(
            DatabaseContext context
        )
        {
            this.context = context;
        }

        #region GetAsync
        public virtual async Task<T> GetAsync(int entityId, bool includeRelated = false)
        {
            T result = null;

            if (!includeRelated)
                result = await context.Set<T>().FindAsync(entityId);
            else
            {
                var query = context.Set<T>().AsQueryable();

                // Relations
                query = this.ApplyRelations(query);

                result = await query.SingleOrDefaultAsync(x => x.Id == entityId);
            }

            return result;
        }
        #endregion

        #region GetAllAsync
        public virtual async Task<BaseQueryResult<T>> GetAllAsync(Q queryModel, bool includeRelated = false)
        {
            var result = new BaseQueryResult<T>();
            var query = context.Set<T>().AsQueryable();

            // Filtering
            query = ApplyFiltering(query, queryModel);

            // Ordering
            query = query.ApplyOrdering(queryModel, ApplyOrderingMap());

            // Count
            result.Count = await query.CountAsync();

            // Paging
            query = query.ApplyPaging(queryModel);

            // Relations
            if (includeRelated)
                query = this.ApplyRelations(query);

            result.Entities = await query.ToArrayAsync();

            return result;
        }
        #endregion

        #region ApplyFiltering
        protected virtual IQueryable<T> ApplyFiltering(IQueryable<T> query, Q queryModel)
        {
            return query;
        }
        #endregion

        #region ApplyOrderingMap
        protected virtual Dictionary<string, Expression<Func<T, object>>> ApplyOrderingMap()
        {
            return new Dictionary<string, Expression<Func<T, object>>>()
            {
                ["id"] = x => x.Id,
            };
        }
        #endregion

        #region ApplyRelations
        protected virtual IQueryable<T> ApplyRelations(IQueryable<T> query)
        {
            return query;
        }
        #endregion

        #region Add
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
        #endregion

        #region Remove
        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        #endregion
    }
}
