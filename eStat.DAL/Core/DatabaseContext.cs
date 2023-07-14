using eStat.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStat.DAL.Core
{
    public class DatabaseContext : DbContext
    {
        private static readonly string connectionString = "Database=EStat;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        public DbSet<Search> Searches { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseProduct> PurchaseProducts { get; set; }
        public DbSet<ProductRequest> ProductRequests { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PriceChange> PriceChanges { get; set; }
        public DbSet<PricePrediction> PricePredictions { get; set; }

        public DatabaseContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
            => dbContext.UseSqlServer(connectionString);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Products).WithOne(u => u.User)
                .HasForeignKey(p => p.UserGUID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
