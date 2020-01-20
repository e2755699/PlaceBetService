using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PlaceBetService
{
    class Recieve
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "10.0.15.40" , UserName = "rabbit", Password = "QCctDqnyu2ig" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("PlaceBet", false, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.Write($"Receive {message}");
                    };
                    channel.BasicConsume("PlaceBet", true, consumer);

                    Thread.Sleep(100000);
                }
            }
        }
    }
}
