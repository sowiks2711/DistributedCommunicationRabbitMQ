using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cymbalists
{
    internal class ComunicationManager
    {
        private readonly NeighboursManager _manager;
        private readonly int _id;
        private readonly string routingName;
        private readonly IConnection _connection;
        private readonly INegotiationController _negotiationController;
        private readonly IModel _sendingChannel;
        //private readonly IEnumerable<IModel> _listenningChannels;

        public ComunicationManager(INegotiationController negotiationController, int id, string routingName)
        {
            this._id = id;
            this.routingName = routingName;
            var factory = new ConnectionFactory(){ HostName = "localhost"};
            this._connection = factory.CreateConnection();
            this._negotiationController = negotiationController;
            this._sendingChannel =_connection.CreateModel();
            this._sendingChannel.ExchangeDeclare(exchange: Program.ExchangeName,
                    type: "direct");
            //this._listenningChannels = new List<IModel>();
        }
        private void SendYourId()
        {
            var message = _id.ToString();
            SendMessage(message);
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _sendingChannel.BasicPublish(exchange: Program.ExchangeName,
                routingKey: routingName,
                basicProperties: null,
                body: body);
            Console.WriteLine($" [{_id}] Sent '{Program.ExchangeName}':'{message}'");

        }

        public void ListenForMessages(NeighbourData neighbour)
        {
            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchange: Program.ExchangeName, type: "direct");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: Program.ExchangeName, routingKey: neighbour.Name);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                _negotiationController.MakeNextMove(message, ea.RoutingKey);

                Console.WriteLine($" [{_id}] Received {message}");
            };

            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
    }
}