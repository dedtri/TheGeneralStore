using Microsoft.EntityFrameworkCore;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;

namespace TheGeneralStore.Backend.Database.Repositories
{
    public class CustomerRepository : BaseRepository<Customer, CustomerQuery>
    {
        public CustomerRepository(DatabaseContext context) : base(context)
        {
        }

        #region GetByNameAsync
        public virtual async Task<Customer> GetByNameAsync(string email, bool includeRelated = false)
        {
            if (!includeRelated)
                return await context.Customers.SingleOrDefaultAsync(x => x.Email == email);

            var query = context.Customers.AsQueryable();

            query = this.ApplyRelations(query);

            return await query.SingleOrDefaultAsync(x => x.Email == email);
        }
        #endregion

        #region ConfirmLoginAsync
        public virtual Customer ConfirmLogin(Customer login, bool includeRelated = false)
        {
            if (!includeRelated)
                return context.Customers.FirstOrDefault(u => (u.Email == login.Email) && (u.Password == login.Password));

            var query = context.Customers.AsQueryable();

            query = this.ApplyRelations(query);

            return query.SingleOrDefault(u => (u.Email == login.Email) && (u.Password == login.Password));
        }
        #endregion
    }
}
