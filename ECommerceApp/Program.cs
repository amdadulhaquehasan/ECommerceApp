using ECommerceApp.BusinessLayer.Modules.Carts;
using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Categories;
using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.BusinessLayer.Modules.Orders;
using ECommerceApp.BusinessLayer.Modules.Orders.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Products;
using ECommerceApp.BusinessLayer.Modules.Products.Interfaces;
using ECommerceApp.DataAccessLayer.Data;
using ECommerceApp.DataAccessLayer.Identity;
using ECommerceApp.DataAccessLayer.Modules.Carts;
using ECommerceApp.DataAccessLayer.Modules.Carts.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Categories;
using ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Orders;
using ECommerceApp.DataAccessLayer.Modules.Orders.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Products;
using ECommerceApp.DataAccessLayer.Modules.Products.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Carts;
using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Categories;
using ECommerceApp.PresentationLayer.Modules.Categories.Interface;
using ECommerceApp.PresentationLayer.Modules.Orders;
using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Products;
using ECommerceApp.PresentationLayer.Modules.Products.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Login related configuration
// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();
// builder.loging.AddDebug();

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
builder.Services.AddSerilog(); 

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddAutoMapper(cfg => { }, typeof(CategoryMappingProfile).Assembly);
builder.Services.AddAutoMapper(cfg => { }, typeof(ProductMappingProfile).Assembly);
builder.Services.AddAutoMapper(cfg => { }, typeof(CartMappingProfile).Assembly);
builder.Services.AddAutoMapper(cfg => { }, typeof(OrderMappingProfile).Assembly);

// Session related services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ECommerceDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IdentityRoleSeeder>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("FullNameOnly", policy => policy.RequireClaim("FullName", "Super Admin")
    .RequireRole("SuperAdmin"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Error/403";
});

builder.Services.AddScoped<ICategoryViewModelProvider, CategoryViewModelProvider>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductViewModelProvider, ProductViewModelProvider>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICartViewModelProvider, CartViewModelProvider>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, SessionCartRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICheckoutViewModelProvider, CheckoutViewModelProvider>();
builder.Services.AddScoped<IOrderViewModelProvider, OrderViewModelProvider>();

var app = builder.Build();

// Logging related code
app.Logger.LogTrace("This is a trace log.");
app.Logger.Log(LogLevel.Debug, "This is a debug log.");
app.Logger.LogInformation("This is an information log.");
app.Logger.LogWarning("This is a warning log.");
app.Logger.LogError("This is an error log.");
app.Logger.LogCritical("This is a critical log.");

app.UseStatusCodePagesWithReExecute("/Error/StatusCode", "?statusCode={0}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<IdentityRoleSeeder>();
    await seed.SeedAsync();
}

app.Run();
