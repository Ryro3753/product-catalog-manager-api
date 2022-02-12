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
        public static Product firstProduct = new()
        {
            Id = new Guid("0448C4E5-6256-4400-89FB-605A6820EC09"),
            Code = "666",
            Name = "Watch",
            Price = 666,
            Picture = "https://picsum.photos/200/300"
        };
        public static  Product secondProduct = new()
        {
            Id = new Guid("5E7BE964-BDBF-438D-AA79-FB324E1AE09E"),
            Code = "665",
            Name = "Second Product",
            Price = 665,
            Picture = "https://picsum.photos/200/300"
        };
        public static Product thirdProduct = new()
        {
            Id = new Guid("868D54E6-5CFD-450F-BA51-760DF652E6D4"),
            Code = "667",
            Name = "Third Product",
            Price = 667,
            Picture = "https://picsum.photos/200/300"
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
