using Confluent.Kafka;
using System;
using Microsoft.Extensions.Configuration;
using System.Threading;

class Producer {
    static void Main(string[] args)
    {
        args = new[] { $"getting-started.properties" };
        if (args.Length != 1) {
            Console.WriteLine("Please provide the configuration file path as a command line argument");
        }

        IConfiguration configuration = new ConfigurationBuilder()
            .AddIniFile(args[0])
            .Build();

        const string topic = "purchases";


        using (var producer = new ProducerBuilder<string, string>(configuration.AsEnumerable()).Build())
        {
            var numProduced = 0;
            Random rnd = new Random();

            while(true)
            {
                var user = $"{numProduced} - {Guid.NewGuid().ToString()}";
                var item = $"{rnd.Next(1000)} - {Guid.NewGuid().ToString()}";

                producer.Produce(topic, new Message<string, string> { Key = user, Value = item },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError) {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else {
                            Console.WriteLine($"Produced event to topic {topic}: key = {user,-10} value = {item}");
                            numProduced += 1;
                        }
                    });

                Thread.Sleep(1000);
            }

            producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
        }
    }
}