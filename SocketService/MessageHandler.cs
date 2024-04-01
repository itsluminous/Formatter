using Confluent.Kafka;

namespace SocketService;

public class MessageHandler(KafkaConfiguration kafkaConfig)
{
    public void Start()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaConfig.BootstrapServers,
            GroupId = kafkaConfig.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        
        var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(kafkaConfig.Topic);

        var clientMessenger = new ClientMessenger();
        
        while (true)
        {
            try
            {
                var consumeResult = consumer.Consume(CancellationToken.None);
                var message = consumeResult.Message.Value;
                    
                Console.WriteLine($"Received message: {message}");
                    
                // Push message to websocket client
                clientMessenger.Send(message);
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error occurred: {e.Error.Reason}");
            }
        }
    }
}