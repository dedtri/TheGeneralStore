namespace TheGeneralStore.Backend.WebAPI.Controllers.Resources
{
    public abstract class BaseResource
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
