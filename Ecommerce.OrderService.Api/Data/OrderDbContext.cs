using Ecommerce.Domain.OrderContext;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.OrderService.Api.Data;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }   
}