using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    public class Reciever
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("BasicTest", false, false, false, null);
                    var Consumer = new EventingBasicConsumer(channel);
                    Consumer.Received += (model, ea) =>
                     {
                         var body = ea.Body.Span;
                         var message = Encoding.UTF8.GetString(body);
                         Console.WriteLine("Recived message{0}...", message);
                     }; 
                    channel.BasicConsume("BasicTest", true, Consumer);
                    Console.WriteLine("Press [Enter] to Exit");
                    Console.ReadLine();
                }
            }
        }
    }
}
