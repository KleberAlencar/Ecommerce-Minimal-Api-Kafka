using Confluent.Kafka;

namespace Ecommerce.OrderService.Api.Kafka;

public interface IProducer
{
    Task ProduceAsync(string topic, Message<string, string> message);   
}