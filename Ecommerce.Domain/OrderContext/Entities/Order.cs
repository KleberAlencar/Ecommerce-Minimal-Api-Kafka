namespace Ecommerce.Domain.OrderContext.Entities;

public record Order
{
    public int Id { get; set; }

    public string CustomerName { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; } = 1;

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;   
}