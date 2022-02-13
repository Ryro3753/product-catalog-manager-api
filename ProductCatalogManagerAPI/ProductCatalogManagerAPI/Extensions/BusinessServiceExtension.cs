using ProductCatalogManagerAPI.Services;

namespace ProductCatalogManagerAPI.Extensions
{
    public static class BusinessServiceExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            //Business Service Implementations
            services.AddTransient<IProductService, ProductService>(); 

            return services;
        }
    }
}
