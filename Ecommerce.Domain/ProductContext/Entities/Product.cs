﻿namespace Ecommerce.Domain.ProductContext.Entities;

public record Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }
}