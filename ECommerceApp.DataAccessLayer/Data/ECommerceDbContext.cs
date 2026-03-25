using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.DataAccessLayer.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Catagories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names
            modelBuilder.Entity<Category>().ToTable("Category");

            modelBuilder.Entity<Product>().ToTable("Product");

            modelBuilder.Entity<Order>().ToTable("Order");

            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");

            modelBuilder.Entity<Customer>().ToTable("Customer");

            modelBuilder.Entity<Payment>().ToTable("Payment");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic gadgets and devices", CreatedDate = new DateTime(2026, 01, 01)},
                new Category { Id = 2, Name = "Books", Description = "Various kinds of books", CreatedDate = new DateTime(2026, 01, 01)},
                new Category { Id = 3, Name = "Clothing", Description = "Apparel and garments", CreatedDate = new DateTime(2026, 01, 01) }
            );

            // Configure Relations Through Fluent API

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
