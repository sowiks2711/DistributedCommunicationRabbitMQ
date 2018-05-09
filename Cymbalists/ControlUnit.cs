using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cymbalists.ActionController.States;
using RabbitMQ.Client.Framing.Impl;

namespace Cymbalists
{
    //enum State
    //{
    //    WRIds,
    //    Eval,
    //    WNWHId,
    //    LWR,
    //    WP
    //}

    enum Message
    {
        Id,
        Lost,
        Won,
        Finished
    }
    public class ControlUnit
    {
        private const string ExchangeName = "cymbalists quarrel";
        internal readonly string RoutingName;
        private readonly IConnection _connection;
        private int Id;
        private readonly NeighboursManager _manager;
        private ControlStateBase state;


        public ControlUnit(IConnection connection, string routingName, NeighboursManager manager)
        {
            var factory = new ConnectionFactory(){ HostName = "localhost"};
            this._connection = factory.CreateConnection();
            this.RoutingName = routingName;
            ///
            /// TODO: create state control machine and assign starting state
            //this.state = new StartingState();
            this._manager = manager;
            this.Id = new Random().Next(int.MaxValue);

        }


        public void Control()
        {
            foreach (var neighbour in _manager.GetAll())
                ListenForMessages(neighbour);
            // create channels for every Neighbour data
            // attach method to message received
            // create your own sender channel 
            ///
            /// TODO: Move this to starting transition action
            SendYourId();
        }

        private void SendYourId()
        {
            var channel = _connection.CreateModel();
                channel.ExchangeDeclare(exchange: ExchangeName,
                    type: "direct");

            var message = RoutingName;
            var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: ExchangeName,
                    routingKey: RoutingName,
                    basicProperties: null,
                    body: body);
            Console.WriteLine(" [x] Sent '{0}':'{1}'", ExchangeName, message);
        }

        private void ListenForMessages(NeighbourData neighbour)
        {
            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: neighbour.Name);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                MakeNextMove(message, ea.RoutingKey);

                Console.WriteLine(" [x] Received {0}", message);
            };

            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }

        private void MakeNextMove(string message, string key)
        {
            _manager.UpdateNeighbourInfo(message, key);
            state = state.TakeAction();
        }
    }
}