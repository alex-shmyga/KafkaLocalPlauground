using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Consumer
{
    public class Function1
    {
        // KafkaTrigger sample 
        // Consume the message from "topic" on the LocalBroker.
        // Add `BrokerList` and `KafkaPassword` to the local.settings.json
        // For EventHubs
        // "BrokerList": "{EVENT_HUBS_NAMESPACE}.servicebus.windows.net:9093"
        // "KafkaPassword":"{EVENT_HUBS_CONNECTION_STRING}
        [FunctionName("Function1")]
        public void Run(
            [KafkaTrigger("localhost:9092",
                          "purchases",
                          AuthenticationMode = BrokerAuthenticationMode.NotSet,
                          ConsumerGroup = "$Default")] KafkaEventData<string, string>[] events,
            ILogger log)
        {
            foreach (KafkaEventData<string, string> eventData in events)
            {
                log.LogInformation($"Key: {eventData. Key} C# Kafka trigger function processed a message: {eventData.Value}");
            }
        }
    }
}
