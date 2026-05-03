using ECommerceApp.BusinessLayer.Modules.Categories;
using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.DataAccessLayer.Data;
using ECommerceApp.DataAccessLayer.Modules.Categories;
using ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Categories;
using ECommerceApp.PresentationLayer.Modules.Categories.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
builder.Host.UseSerilog(Log.Logger);

builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddAutoMapper(cfg => { }, typeof(CategoryMappingProfile).Assembly);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryViewModelProvider, CategoryViewModelProvider>();

var app = builder.Build();

app.Logger.LogInformation("This is an information log.");
app.Logger.LogWarning("This is a warning log.");
app.Logger.LogError("This is an error log.");
app.Logger.LogCritical("This is a critical log.");

//var products = new List<Product>
//{
//    new Product { Id = 1, Name = "Laptop", Price = 100000 },
//    new Product { Id = 2, Name = "Mobile", Price = 20000 },
//    new Product { Id = 3, Name = "Book", Price = 1500 }
//};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseRouting();

#region Minimal API
//// Get all products
//app.MapGet("/products", () =>
//{
//    return products;
//});

//// Get product by id
//app.MapGet("/products/{id}", (int id) =>
//{
//    var product = products.FirstOrDefault(p => p.Id == id);
//    return product;
//});

//// Post
//app.MapPost("/products/", (Product product) =>
//{
//    product.Id = products.Max(p => p.Id) + 1;
//    products.Add(product);
//    return product;
//});

//// put
//app.MapPut("/products/{id}", (int id, Product updateProduct) =>
//{
//    var product = products.FirstOrDefault(p => p.Id == id);
//    if (product is null)
//    {
//        return null;
//    }
//    product.Name = updateProduct.Name;
//    product.Price = updateProduct.Price;
//    return product;
//});

//// patch
//app.MapPatch("/products/{id}", (int id, Product updateProduct) =>
//{
//    var product = products.FirstOrDefault(p => p.Id == id);
//    if (product is null) 
//    {
//        return null;
//    }
//    if (!string.IsNullOrEmpty(updateProduct.Name))
//    {
//        product.Name = updateProduct.Name;
//    }
//    if (updateProduct.Price > 0)
//    {
//        product.Price = updateProduct.Price;
//    }
//    return product;
//});

//// Delete
//app.MapDelete("/products/{id}", (int id) =>
//{
//    var product = products.FirstOrDefault(p => p.Id == id);
//    if (product is null)
//    {
//        return null;
//    }
//    products.Remove(product);
//    return products;
//});
#endregion

app.MapControllers();

app.Run();

//public class Product
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public decimal Price { get; set; }
//}
