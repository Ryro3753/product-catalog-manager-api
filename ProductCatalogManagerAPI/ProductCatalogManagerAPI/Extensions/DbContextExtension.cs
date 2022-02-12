using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogManagerAPI.Database;
using ProductCatalogManagerAPI.Models;

namespace ProductCatalogManagerAPI.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddDbContextExtension(this IServiceCollection services)
        {
            services.AddDbContext<ProductContext>(db => db.UseInMemoryDatabase("ProductDatabase"));
            return services;
        }
    }
    public static class SeedData
    {
        public static Product firstProduct = new Product
        {
            Code = "666",
            Name = "First Product",
            Price = 666
        };
        public static  Product secondProduct = new Product
        {
            Code = "665",
            Name = "Second Product",
            Price = 665
        };
        public static Product thirdProduct = new Product
        {
            Code = "667",
            Name = "Third Product",
            Price = 667
        };
        public static WebApplication SeedDbContext<TContext>(this WebApplication webApp) where TContext : DbContext
        {
            using (var scope = webApp.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TContext>();

                context.Database.EnsureCreated();
            }

            return webApp;
        }
    }
}
