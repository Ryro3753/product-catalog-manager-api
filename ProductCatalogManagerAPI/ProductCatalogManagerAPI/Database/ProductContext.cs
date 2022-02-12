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
            optionsBuilder.UseInMemoryDatabase("ProductDatabase");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           modelBuilder.Entity<Product>().HasData(
            SeedData.firstProduct,
            SeedData.secondProduct,
            SeedData.thirdProduct);
            base.OnModelCreating(modelBuilder);

        }


    }
}
