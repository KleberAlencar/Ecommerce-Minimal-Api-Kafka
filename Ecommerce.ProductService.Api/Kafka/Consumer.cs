using Confluent.Kafka;
using Newtonsoft.Json;
using Ecommerce.ProductService.Api.Data;
using Ecommerce.Domain.OrderContext.Entities;

namespace Ecommerce.ProductService.Api.Kafka;

public class Consumer(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            _ = ConsumeAsync("order-topic", cancellationToken);
        }, cancellationToken);
    }

    public async Task ConsumeAsync(string topic, CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = "order-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        
        consumer.Subscribe(topic);
        while (!cancellationToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(cancellationToken);

            var order = JsonConvert.DeserializeObject<OrderMessage>(consumeResult.Message.Value);
            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

            var product = await dbContext.Products.FindAsync(order.ProductId);
            if (product != null)
            {
                product.Quantity -= order.Quantity;
                await dbContext.SaveChangesAsync();
            }
        }
        consumer.Close();
    }
}