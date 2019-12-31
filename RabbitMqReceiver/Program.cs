using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Receiver app now works!");

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "msgKey",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("[x] received {0}", message);
                };

                channel.BasicConsume(queue: "msgKey",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine("press key to exit");
                Console.ReadLine();
            }

            
        }
    }
}
