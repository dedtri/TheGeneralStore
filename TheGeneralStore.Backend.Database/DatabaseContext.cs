using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TheGeneralStore.Backend.Database.DataModels;

namespace TheGeneralStore.Backend.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        #region DbSets

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        // Lecture Testing
        //public DbSet<Hero> Heroes { get; set; }
        //public DbSet<Team> Teams { get; set; }
        #endregion

        #region OnModelCreating - override
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 1,
                FirstName = "Admin",
                Email = "admin@TGS.dk",
                Password = "123456",
                Role = "Admin"
            });

            modelBuilder.Entity<Customer>()
                 .Property(t => t.Role)
                .HasDefaultValue("User");

            modelBuilder.Entity<Customer>().HasIndex(u => u.Email).IsUnique();

            base.OnModelCreating(modelBuilder);
            this.ApplyIsDeletedGlobalFilter(modelBuilder);
        }
        #endregion

        #region ApplyIsDeletedGlobalFilter
        private void ApplyIsDeletedGlobalFilter(ModelBuilder modelBuilder)
        {
            // Creating lamda expression
            Expression<Func<BaseDataModel, bool>> filterExpr = x => x.IsDeleted == false;

            // Getting all current models in Entity
            var entityTypes = modelBuilder.Model.GetEntityTypes();

            // Applying IsDeleted Global Filter
            foreach (var entityType in entityTypes)
            {
                // Check if type of TTBaseDataModel
                if (!entityType.ClrType.IsAssignableTo(typeof(BaseDataModel)))
                    continue;

                // Modify expression to handle correct child type  
                var parameter = Expression.Parameter(entityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);

                // Set filter  
                entityType.SetQueryFilter(lambdaExpression);
            }
        }
        #endregion
    }
}
