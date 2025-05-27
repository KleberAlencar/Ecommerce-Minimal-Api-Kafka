using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.OrderContext;
using Ecommerce.OrderService.Api.Data;

namespace Ecommerce.OrderService.Api.Endpoints;

public static class OrderEndpoint
{
    public static RouteGroupBuilder AddMapOrderEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (OrderDbContext context) 
            => await context.Orders.ToListAsync());
        
        group.MapGet("/{id:int}", async (OrderDbContext context, int id) =>
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null) Results.NotFound();
            
            return Results.Ok(order);
        });

        group.MapPost("/", async (OrderDbContext context, Order order) =>
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            return Results.Created($"/orders/{order.Id}", order);       
        });

        group.MapPut("/{id:int}", async (OrderDbContext context, int id, Order order) =>
        {
            var existingOrder = await context.Orders.FindAsync(id);
            if (existingOrder == null) Results.NotFound();
            
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.OrderDate = DateTime.UtcNow;
            existingOrder.ProductId = order.ProductId;
            existingOrder.Quantity = order.Quantity;
           
            await context.SaveChangesAsync();

            return Results.Ok(existingOrder);       
        });

        group.MapDelete("/{id:int}", async (OrderDbContext context, int id) =>
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null) Results.NotFound();
            
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
            
            return Results.Ok();      
        });
        
        return group;
    }
}