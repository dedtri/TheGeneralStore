using Microsoft.EntityFrameworkCore;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public class ImageRepository : BaseRepository<Image, ImageQuery>
    {
        public ImageRepository(DatabaseContext context) : base(context)
        {
        }

        #region ApplyRelations
        protected override IQueryable<Image> ApplyRelations(IQueryable<Image> query)
        {
            query = query
                .Include(x => x.Product);

            return base.ApplyRelations(query);
        }
        #endregion

        protected override IQueryable<Image> ApplyFiltering(IQueryable<Image> query, ImageQuery queryModel)
        {
            query = query.Where(image => image.Product != null);

            return base.ApplyFiltering(query, queryModel);
        }
    }
}
