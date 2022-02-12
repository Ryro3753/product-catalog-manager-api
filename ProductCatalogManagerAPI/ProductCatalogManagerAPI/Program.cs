using Microsoft.EntityFrameworkCore;
using ProductCatalogManagerAPI.Database;
using ProductCatalogManagerAPI.Extensions;
using ProductCatalogManagerAPI.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersion();
builder.Services.AddDbContextExtension();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.AddSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.SeedDbContext<ProductContext>();

app.Run();
