using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMq.Consumer
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
            Console.WriteLine("FANOUT EXCHANGE");

            var factory = new ConnectionFactory();

            factory.Uri = new Uri("amqps://avjbdlwa:yDeYicx9stGsfLYwSBplmTnmqZS9iAbh@baboon.rmq.cloudamqp.com/avjbdlwa");
            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            var QueueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(QueueName, exchange: "Fanout", "", null);

            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(QueueName, false, consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Incoming Message : " + message);

                channel.BasicAck(e.DeliveryTag, false);
            };
        }
    }

    public static class DirectExchange 
    {
        public static void Start()
        {
            Console.WriteLine("FANOUT EXCHANGE");

            var factory = new ConnectionFactory();

            factory.Uri = new Uri("amqps://avjbdlwa:yDeYicx9stGsfLYwSBplmTnmqZS9iAbh@baboon.rmq.cloudamqp.com/avjbdlwa");
            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            var queueName = "direct-queue-Info"; // Dinlemek istedigimiz queuenin adina gore 

            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queueName, false, consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Incoming Message : " + message);

                channel.BasicAck(e.DeliveryTag, false);
            };
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
