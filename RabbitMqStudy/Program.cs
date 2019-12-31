using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sender app now works!");

            var factory = new ConnectionFactory() { HostName = "localhost"};
            
            using(var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "msgKey",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                while (true)
                {
                    Console.WriteLine("Enter message to send:");
                    var msg = Console.ReadLine();
                    var body = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish(exchange: "",
                        routingKey: "msgKey",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine("[x] sent {0}", msg);
                }
            }

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
