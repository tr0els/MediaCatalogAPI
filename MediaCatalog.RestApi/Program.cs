using Azure.Storage.Blobs;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Interfaces;
using MediaCatalog.Core.Services;
using MediaCatalog.Infrastructure;
using MediaCatalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
//builder.Services.AddDbContext<ProductCatalogContext>(opt => opt.UseInMemoryDatabase("ProductCatalogDb"));
builder.Services.AddDbContext<ProductCatalogContext>(opt => opt.UseSqlite("Data Source=App.db"));
builder.Services.AddScoped<IRepository<Catalog>, CatalogRepository>();
builder.Services.AddScoped<IProductRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Image>, ImageRepository>();
builder.Services.AddScoped<IRepository<ImageVariant>, ImageVariantRepository>();
builder.Services.AddScoped<ICatalogManager, CatalogManager>();
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped(x => new BlobServiceClient(configuration["AzureBlobStorage"]));
builder.Services.AddScoped<IBlobRepository, BlobRepository>();
builder.Services.AddTransient<IDbInitializer, DbInitializer>();

// Ignore possible object reference cycles at runtime
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Setup CORS
app.UseCors(builder =>
{
    builder
          .WithOrigins("http://localhost:4200", "https://localhost:4200")
          .SetIsOriginAllowedToAllowWildcardSubdomains()
          .AllowAnyHeader()
          .AllowCredentials()
          .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
          .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
}
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Initialize the database.
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetService<ProductCatalogContext>();
        var dbInitializer = services.GetService<IDbInitializer>();
        dbInitializer.Initialize(dbContext);
    }
} else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();