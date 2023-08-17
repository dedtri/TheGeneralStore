namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources
{
    public class BaseQueryResultResource<T>
    {
        public IEnumerable<T> Entities { get; set; }
        public int Count { get; set; }
    }
    
}
