using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.ProductContext;

namespace Ecommerce.ProductService.Api.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "Shirt", Quantity = 100, Price = 20 });
        modelBuilder.Entity<Product>().HasData(new Product { Id = 2, Name = "Pant", Quantity = 100, Price = 50 });
        modelBuilder.Entity<Product>().HasData(new Product { Id = 3, Name = "Polo", Quantity = 100, Price = 100 });
        base.OnModelCreating(modelBuilder);
    }    
    
    public DbSet<Product> Products { get; set; }
}