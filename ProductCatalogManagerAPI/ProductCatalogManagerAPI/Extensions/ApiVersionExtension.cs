using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogManagerAPI.Extensions
{
    public static class ApiVersionExtension
    {
        public static IServiceCollection AddVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            return services;
        }
    }
}
