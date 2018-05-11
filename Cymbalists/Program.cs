using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Cymbalists.InitializationHelpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cymbalists
{
    class Program
    {
        internal const string HostName = "localhost";
        internal static readonly string PositionFilePath = "Resources/positions.txt";
        internal static readonly string QueueName = "Queueu";
        public const string ExchangeName = "cymbalists quarrel";
        internal static readonly double HearingDistance = 3;
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory(){ HostName = HostName };
            var threads = new List<Thread>();
            
                var nodes = new NodesConnector(new NodesFactory().Create() ).Connect();
                foreach (var node in nodes)
                {
                    var controlUnit = node.CreateControlUnit(factory);
                    ///
                    /// TODO: create threads and pass them the appropriate objects method
                    ///
                    var thread = new Thread(controlUnit.Control);
                    thread.Start();
                    threads.Add(thread);
                }
                //var nodes = new NodesConnector(ReadCoordinates(connection) ).Connect();
            

            foreach (var thread in threads)
                thread.Join();
            //Thread receiver = new Thread(receiverMethod);
            //Thread sender = new Thread(senderMethod);
            //receiver.Start();
            //sender.Start();
        }


        private static void SenderMethod()
        {
            
            var factory = new ConnectionFactory(){ HostName = HostName };
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
            var factory = new ConnectionFactory() {HostName = HostName };
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
