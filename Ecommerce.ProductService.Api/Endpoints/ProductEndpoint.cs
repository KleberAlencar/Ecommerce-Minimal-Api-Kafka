using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.ProductContext;
using Ecommerce.ProductService.Api.Data;

namespace Ecommerce.ProductService.Api.Endpoints;

public static class ProductEndpoint
{
    public static RouteGroupBuilder AddMapProductEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (ProductDbContext context) 
            => await context.Products.ToListAsync());
        
        group.MapGet("/{id:int}", async (ProductDbContext context, int id) =>
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) Results.NotFound();
            
            return Results.Ok(product);
        });

        group.MapPost("/", async (ProductDbContext context, Product product) =>
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return Results.Created($"/products/{product.Id}", product);       
        });

        group.MapPut("/{id:int}", async (ProductDbContext context, int id, Product product) =>
        {
            var existingProduct = await context.Products.FindAsync(id);
            if (existingProduct == null) Results.NotFound();
            
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
           
            await context.SaveChangesAsync();

            return Results.Ok(existingProduct);       
        });

        group.MapDelete("/{id:int}", async (ProductDbContext context, int id) =>
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) Results.NotFound();
            
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            
            return Results.Ok();      
        });
        
        return group;
    }   
}