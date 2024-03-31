using Confluent.Kafka;

namespace ListenerService;

public class MessageHandler
{
    private readonly IProducer<Null, string> _producer;
    private readonly string _topic;

    public MessageHandler(string brokerList, string topic)
    {
        var config = new ProducerConfig { BootstrapServers = brokerList };
        _producer = new ProducerBuilder<Null, string>(config).Build();
        _topic = topic;
    }

    public async void SendMessageAsync(string message)
    {
        try
        {
            var deliveryReport = await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
            Console.WriteLine($"Delivered '{deliveryReport.Value}' to '{deliveryReport.TopicPartitionOffset}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to deliver message: {ex.Message}");
        }
    }

    public void Close()
    {
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
    }
}