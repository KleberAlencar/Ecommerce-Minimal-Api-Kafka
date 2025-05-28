using Confluent.Kafka;

namespace Ecommerce.OrderService.Api.Kafka;

public class Producer : IProducer
{
    private readonly IProducer<string, string> _producer;

    public Producer()
    {
        var config = new ConsumerConfig();
        config.GroupId = "order-group";
        config.BootstrapServers = "localhost:9092";
        config.AutoOffsetReset = AutoOffsetReset.Earliest;
        
        _producer = new ProducerBuilder<string, string>(config).Build();
    }
        
    public Task ProduceAsync(string topic, Message<string, string> message)
    {
        return _producer.ProduceAsync(topic, message);       
    }
}