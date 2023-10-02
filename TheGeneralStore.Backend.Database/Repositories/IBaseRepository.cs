using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public interface IBaseRepository<T, Q>
        where T : BaseDataModel, new()
        where Q : BaseQueryModel
    {
        Task<T> GetAsync(int entityId, bool includeRelated = false);

        Task<BaseQueryResult<T>> GetAllAsync(Q queryModel, bool includeRelated = false);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}