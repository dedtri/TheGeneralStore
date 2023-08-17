using System.Linq.Expressions;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Core.Extensions
{
    public static class QueryableExensions
    {
        #region ApplyOrdering
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, BaseQueryModel queryModel, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (string.IsNullOrWhiteSpace(queryModel.SortBy) || !columnsMap.ContainsKey(queryModel.SortBy))
                return query;

            if (queryModel.IsSortAscending)
                return query.OrderBy(columnsMap[queryModel.SortBy]);
            else
                return query.OrderByDescending(columnsMap[queryModel.SortBy]);
        }
        #endregion

        #region ApplyPaging
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, BaseQueryModel queryModel)
        {
            // No paging
            if (queryModel.PageSize == -1)
                return query;

            // Default paging paramenters
            if (queryModel.Page <= 0)
                queryModel.Page = 1;

            if (queryModel.PageSize <= 0)
                queryModel.PageSize = 10;

            return query.Skip((queryModel.Page - 1) * queryModel.PageSize).Take(queryModel.PageSize);
        }
        #endregion
    }
}
