﻿namespace Ecommerce.Domain.OrderContext.Entities;

public class OrderMessage
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}