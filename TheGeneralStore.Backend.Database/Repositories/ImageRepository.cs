using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public class ImageRepository : BaseRepository<Image, ImageQuery>
    {
        public ImageRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
