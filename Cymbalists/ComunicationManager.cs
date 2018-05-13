using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cymbalists
{
    public class ComunicationManager
    {
        public int Id { get; }
        public bool FinishedAlreadySend { get; set; }

        private readonly string _routingName;
        private readonly INegotiationController _negotiationController;
        private readonly IModel _channel;
        private readonly List<string> _consumerTags;

        private bool _isClosing;

        private object isClosingLock;
        //private readonly IEnumerable<IModel> _listenningChannels;

        public ComunicationManager(INegotiationController negotiationController, int id, string routingName)
        {
            this.Id = id;
            this._routingName = routingName;
            var factory = new ConnectionFactory(){ HostName = "localhost"};
            var connection = factory.CreateConnection();
            this._negotiationController = negotiationController;
            this._channel =connection.CreateModel();
            this._channel.ExchangeDeclare(exchange: Program.ExchangeName,
                    type: "direct");
            this._consumerTags = new List<string>();
            this._isClosing = false;
            this.isClosingLock = new object();
            this.FinishedAlreadySend = false;
            //this._listenningChannels = new List<IModel>();
        }
        public void SendId()
        {
            var message = Id.ToString();
            SendMessage(message);
        }
        public void SendLost()
        {
            var message = Message.Lost.ToString();
            SendMessage(message);
        }

        public void SendWon()
        {
            var message = Message.Won.ToString();
            SendMessage(message);
        }

        public void SendFinished()
        {
            var message = Message.Finished.ToString();
            SendMessage(message);
            lock (isClosingLock)
            {
                foreach (var consumerTag in _consumerTags)
                {
                    _channel.BasicCancel(consumerTag);
                }
                _channel.Close();
                _isClosing = true;
                FinishedAlreadySend = true;
            }
        }

        public void ListenForMessages(NeighbourData neighbour)
        {
            //var channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: Program.ExchangeName, type: "direct");
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: Program.ExchangeName, routingKey: neighbour.Name);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                lock (isClosingLock)
                {
                    if (_isClosing) return;
                }
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Program.logger.LoggMessageReceived(Id, message);
                _negotiationController.MakeNextMove(message, ea.RoutingKey);

            };
            _consumerTags.Add(
                _channel.BasicConsume(queue: queueName,
                    autoAck: true,
                    consumer: consumer)
            );

        }
        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            Program.logger.LoggMessageSent(Id, message);
            _channel.BasicPublish(exchange: Program.ExchangeName,
                routingKey: _routingName,
                basicProperties: null,
                body: body);
            

        }
    }
}