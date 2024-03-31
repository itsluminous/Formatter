using Confluent.Kafka;

namespace ParserService
{
    public class ParserService(KafkaConfiguration kafkaConfig)
    {
        public void Start()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaConfig.BootstrapServers,
                GroupId = kafkaConfig.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(kafkaConfig.IncomingTopic);

            Console.WriteLine($"ParserService is listening to topic: {kafkaConfig.IncomingTopic}");
            
            var producerConfig = new ProducerConfig { BootstrapServers = kafkaConfig.BootstrapServers };
            using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();

            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume(CancellationToken.None);
                    var message = consumeResult.Message.Value;
                    var updatedMessage = Mapper.ReplaceNums(message);
                    
                    Console.WriteLine($"Received message: {message}");
                    
                    // Publish updated message to another topic
                    var deliveryReport = producer.ProduceAsync(kafkaConfig.OutgoingTopic, new Message<Null, string> { Value = updatedMessage }).Result;
                    Console.WriteLine($"Published message: {updatedMessage}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occurred: {e.Error.Reason}");
                }
            }
        }
    }
}