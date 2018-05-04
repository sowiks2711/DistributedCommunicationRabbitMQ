using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cymbalists
{
    class Program
    {
        public static readonly double HearingDistance = 3;
        public static void Main(string[] args)
        {
            var nodes = new GraphBuilder(ReadCoordinates()).Build();
            

            //Thread receiver = new Thread(receiverMethod);
            //Thread sender = new Thread(senderMethod);
            //receiver.Start();
            //sender.Start();
        }

        private static List<Node> ReadCoordinates()
        {
            var nodes = new List<Node>();
            using (TextReader reader = File.OpenText("Resources/positions.txt"))
            {
                var n = int.Parse(reader.ReadLine());
                for (var i = 0; i < n; i++)
                {
                    var coordinates = reader.ReadLine().Split(' ');
                    nodes.Add(new Node(int.Parse(coordinates[0]), int.Parse(coordinates[1])));
                }
            }

            return nodes;
        }

        private static void SenderMethod()
        {
            
            var factory = new ConnectionFactory(){ HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
        private static void ReceiverMethod()
        {
            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };

                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();

            }
        }
    }
}
