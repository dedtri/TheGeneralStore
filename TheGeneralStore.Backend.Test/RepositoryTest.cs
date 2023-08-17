using Microsoft.EntityFrameworkCore;
using TheGeneralStore.Backend.Database;
using TheGeneralStore.Backend.Database.DataModels;
using TheGeneralStore.Backend.Database.QueryModels;
using TheGeneralStore.Backend.Database.Repositories;

namespace TheGeneralStore.Backend.Test
{
    public class RepositoryTest
    {
        protected DatabaseContext context;
        public DbContextOptions<DatabaseContext> options;

        public RepositoryTest()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "dummyDatabase")
                .Options;
            context = new DatabaseContext(options);
            context.Database.EnsureDeleted();
            context.Customers.Add(new Customer
            {
                Id = 1,
                FirstName = "Lars",
                LastName = "Naa",
                Email = "L@N.dk",
            });
            context.Customers.Add(new Customer
            {
                Id = 5,
                FirstName = "Bo",
                LastName = "Tammer",
                Email = "B@T.dk",
            });
            context.SaveChanges();
        }

        [Fact]
        public async void IsGetAllNotNull()
        {
            // Testing context
            //context = null;

            // Arrange - Set some things
            var query = new CustomerQuery()
            {
                PageSize = -1
            };

            CustomerRepository repo = new CustomerRepository(context);

            // Act - Invoke Methods
            var result = await repo.GetAllAsync(query);
            
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task IsGetAllNull()
        {
            // Testing context
            context = null;

            // Arrange - Set some things
            var query = new CustomerQuery()
            {
                PageSize = -1
            };

            CustomerRepository repo = new CustomerRepository(context);

            // Act - Invoke Methods
            var result = await repo.GetAllAsync(query);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteReturnTrue()
        {
            var entityToRemove = await context.Customers.FirstOrDefaultAsync(h => h.Id == 1);
            CustomerRepository repo = new CustomerRepository(context);
            repo.Remove(entityToRemove);
            await context.SaveChangesAsync();

            var entityAfterRemoval = await context.Customers.FirstOrDefaultAsync(h => h.Id == 1);

            Assert.True(entityAfterRemoval == null);
        }

        [Fact]
        public async Task CreateReturnNotNull()
        {

            var entityToAdd = new Customer
            {
                Id = 88,
                FirstName = "Martin",
                LastName = "Apso",
                Email = "M@A.dk",
            };

            CustomerRepository repo = new CustomerRepository(context);
            repo.Add(entityToAdd);
            await context.SaveChangesAsync();

            var entityAfterCreation = await context.Customers.FirstOrDefaultAsync(h => h.Id == entityToAdd.Id);

            Assert.NotNull(entityAfterCreation);
        }


    }
}
