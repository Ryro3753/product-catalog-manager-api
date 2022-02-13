using Microsoft.EntityFrameworkCore;
using ProductCatalogManagerAPI.Extensions;
using ProductCatalogManagerAPI.Models;

namespace ProductCatalogManagerAPI.Database
{
    public class ProductContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Using in memory database for easy use. It has to be changed in production level
            optionsBuilder.UseInMemoryDatabase("ProductDatabase");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Adding seed data to db context
           modelBuilder.Entity<Product>().HasData(
            SeedData.firstProduct,
            SeedData.secondProduct,
            SeedData.thirdProduct);
            base.OnModelCreating(modelBuilder);

        }


    }
}
