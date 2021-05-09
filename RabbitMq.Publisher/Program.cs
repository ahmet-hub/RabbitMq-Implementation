using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace RabbitMq.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            //FanoutExchange.Start();
            //DirectExchange.Start();
            //TopicExchange.Start();
            //HeaderExchange.Start();


            Console.ReadLine();

        }
    }

    public static class FanoutExchange
    {
        public static void Start()
        {
            var factory = new ConnectionFactory();

            factory.Uri = new Uri("amqps://avjbdlwa:yDeYicx9stGsfLYwSBplmTnmqZS9iAbh@baboon.rmq.cloudamqp.com/avjbdlwa");
            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare("Fanout", durable: true, type: ExchangeType.Fanout);

            for (int i = 1; i <= 100; i++)
            {
                var message = $"Fanout Message {i}";

                var messageBody = Encoding.UTF8.GetBytes(message);

                Thread.Sleep(1);
                channel.BasicPublish("Fanout", "", null, messageBody);
                Console.WriteLine("Outgoing Message : " + message);

            }

        }
    }
    public static class DirectExchange
    {
        //Her bir log icin route ve kuyruk olusturmak gerekir.
        public enum LogNames
        {
            Error = 1,
            Warning = 2,
            Info = 3
        }

        public static void Start()
        {
            var factory = new ConnectionFactory();

            factory.Uri = new Uri("amqps://avjbdlwa:yDeYicx9stGsfLYwSBplmTnmqZS9iAbh@baboon.rmq.cloudamqp.com/avjbdlwa");
            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare("Direct", durable: true, type: ExchangeType.Direct);

            Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
            {
                var queueName = $"direct-queue-{x}";
                var routeKey = $"route-{x}";
                channel.QueueDeclare(queueName, true, false, false);

                channel.QueueBind(queueName, "Direct", routeKey, null);
            });

            for (int i = 1; i <= 100; i++)
            {
                LogNames log = (LogNames)new Random().Next(1, 4);
                var message = $"Log Type : {log}";

                var messageBody = Encoding.UTF8.GetBytes(message);
                var routeKey = $"route-{log}";

                Thread.Sleep(1);

                channel.BasicPublish("Direct", routeKey, null, messageBody);

                Console.WriteLine("Outgoing Message : " + message);

            }

        }
    }
    public static class TopicExchange
    {
        // TODO : Implementation 
        public static void Start()
        {

        }
    }
    public static class HeaderExchange
    {
        // TODO : Implementation 
        public static void Start()
        {

        }
    }

}
