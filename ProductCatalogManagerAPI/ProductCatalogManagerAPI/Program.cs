using ProductCatalogManagerAPI.Database;
using ProductCatalogManagerAPI.Extensions;
using ProductCatalogManagerAPI.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersion();
builder.Services.AddDbContextExtension();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Metrics extension
builder.Services.AddMetricsExtension();
builder.Host.AddWebTracking();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.AddSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.SeedDbContext<ProductContext>();

app.UseMetrics();

app.Run();


public partial class Program
{
    // Expose the Program class for use with WebApplicationFactory<T>
}