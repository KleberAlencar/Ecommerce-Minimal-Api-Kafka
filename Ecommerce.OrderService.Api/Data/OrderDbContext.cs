using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.OrderContext.Entities;

namespace Ecommerce.OrderService.Api.Data;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }   
}