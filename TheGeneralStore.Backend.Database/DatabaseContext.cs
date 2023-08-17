using Microsoft.EntityFrameworkCore;
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
        public DbSet<Login> Logins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        // Lecture Testing
        //public DbSet<Hero> Heroes { get; set; }
        //public DbSet<Team> Teams { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
