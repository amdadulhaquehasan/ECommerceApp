using ECommerceApp.BusinessLayer.Modules.Categories;
using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.DataAccessLayer.Data;
using ECommerceApp.DataAccessLayer.Modules.Categories;
using ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Categories;
using ECommerceApp.PresentationLayer.Modules.Categories.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<ICategoryViewModelProvider, CategoryViewModelProvider>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/Error/StatusCode", "?Statuscode={0}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
