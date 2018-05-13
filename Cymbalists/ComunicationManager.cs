using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cymbalists
{
    public class ComunicationManager
    {
        private readonly IModel _channel;
        private readonly List<string> _consumerTags;
        private readonly object _isClosingLock;
        private readonly INegotiationController _negotiationController;

        private readonly string _routingName;

        private bool _isClosing;

        public ComunicationManager(INegotiationController negotiationController, int id, string routingName)
        {
            Id = id;
            _routingName = routingName;
            var factory = new ConnectionFactory {HostName = "localhost"};
            var connection = factory.CreateConnection();
            _negotiationController = negotiationController;
            _channel = connection.CreateModel();
            _channel.ExchangeDeclare(Program.ExchangeName,
                "direct");
            _consumerTags = new List<string>();
            _isClosing = false;
            _isClosingLock = new object();
            FinishedAlreadySend = false;
        }

        public int Id { get; }
        public bool FinishedAlreadySend { get; set; }

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
            lock (_isClosingLock)
            {
                foreach (var consumerTag in _consumerTags) _channel.BasicCancel(consumerTag);
                _channel.Close();
                _isClosing = true;
                FinishedAlreadySend = true;
            }
        }

        public void ListenForMessages(NeighbourData neighbour)
        {
            _channel.ExchangeDeclare(Program.ExchangeName, "direct");
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queueName, Program.ExchangeName, neighbour.Name);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                lock (_isClosingLock)
                {
                    if (_isClosing) return;
                }

                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Program.Logger.LoggMessageReceived(Id, message);
                _negotiationController.MakeNextMove(message, ea.RoutingKey);
            };
            _consumerTags.Add(
                _channel.BasicConsume(queueName,
                    true,
                    consumer)
            );
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            Program.Logger.LoggMessageSent(Id, message);
            _channel.BasicPublish(Program.ExchangeName,
                _routingName,
                null,
                body);
        }
    }
}