using Microsoft.Extensions.Configuration;

namespace ParserService
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = LoadConfiguration();

            var kafkaConfig = new KafkaConfiguration
            {
                BootstrapServers = config["Kafka:BrokerList"],
                IncomingTopic = config["Kafka:IncomingTopic"],
                OutgoingTopic = config["Kafka:OutgoingTopic"],
                GroupId = config["Kafka:GroupId"]
            };

            var parserService = new ParserService(kafkaConfig);
            parserService.Start();
        }

        private static IConfigurationRoot LoadConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("BUILD_CONFIGURATION") ?? "Development";

            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();

            return configBuilder.Build();
        }
    }
}