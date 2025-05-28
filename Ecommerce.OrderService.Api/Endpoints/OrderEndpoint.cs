using System.Text.Json.Serialization;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Ecommerce.OrderService.Api.Data;
using Ecommerce.Domain.OrderContext.Entities;
using Ecommerce.OrderService.Api.Kafka;
using Newtonsoft.Json;

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

        group.MapPost("/", async (OrderDbContext context, Order order, IProducer producer) =>
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var orderMessage = new OrderMessage
            {
                OrderId = order.Id,
                ProductId = order.ProductId,
                Quantity = order.Quantity
            };
            
            await producer.ProduceAsync("order-topic", new Message<string, string>()
            {
                Key = order.Id.ToString(), 
                Value = JsonConvert.SerializeObject(orderMessage)
            });
            
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