using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Catagories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table name
            modelBuilder.Entity<Category>().ToTable("Category");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic gadgets and devices", CreatedDate = new DateTime(2026, 01, 01)},
                new Category { Id = 2, Name = "Books", Description = "Various kinds of books", CreatedDate = new DateTime(2026, 01, 01)},
                new Category { Id = 3, Name = "Clothing", Description = "Apparel and garments", CreatedDate = new DateTime(2026, 01, 01) }
            );

        }
    }
}
