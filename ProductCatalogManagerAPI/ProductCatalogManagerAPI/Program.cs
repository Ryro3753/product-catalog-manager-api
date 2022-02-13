using ProductCatalogManagerAPI.Database;
using ProductCatalogManagerAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersion(); // Version Extension
builder.Services.AddDbContextExtension(); //DBContext Extension
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddBusinessServices(); // Business Service Extension

//Metrics extension
builder.Services.AddMetricsExtension();
builder.Host.AddWebTracking();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.AddSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.SeedDbContext<ProductContext>(); //Seeding initial data

app.UseMetrics(); //For Metrics

app.Run();


public partial class Program
{
    // Expose the Program class for use with WebApplicationFactory<T>
}