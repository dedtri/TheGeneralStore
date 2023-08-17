namespace TheGeneralStore.Backend.Database.QueryModels
{
    public class BaseQueryResult<T>
    {
        public IEnumerable<T> Entities { get; set; }
        public int Count { get; set; }
    }
}
