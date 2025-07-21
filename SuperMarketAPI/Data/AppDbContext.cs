using Microsoft.EntityFrameworkCore;
using SuperMarketAPI.Models;

namespace SuperMarketAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet properties for your domain models
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        // Add other DbSet properties for your business entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure your entity relationships and constraints here
        }
    }
}
