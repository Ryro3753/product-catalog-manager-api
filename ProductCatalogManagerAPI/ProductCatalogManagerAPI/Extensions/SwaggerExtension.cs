using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ProductCatalogManagerAPI.Extensions
{
    public static class SwaggerExtension
    {
        public static WebApplication AddSwagger(this WebApplication webApp)
        {
            var apiVersionDescriptionProvider = webApp.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            webApp.UseSwagger();
            webApp.UseSwaggerUI(
                options =>
                {
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            return webApp;

        }
    }
}
