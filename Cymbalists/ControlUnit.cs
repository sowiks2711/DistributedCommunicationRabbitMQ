using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client.Framing.Impl;

namespace Cymbalists
{
    enum State
    {
        WRIds,
        Eval,
        WNWHId,
        LWR,
        WP
    }

    enum Message
    {
        Id,
        Lost,
        Won,
        Finished
    }
    internal class ControlUnit
    {
        private const string ExchangeName = "cymbalists quarrel";
        internal readonly string RoutingName;
        private readonly List<NeighbourData> _neighbours;
        private readonly Dictionary<string, NeighbourData> _neighboursDict;
        private readonly IConnection _connection;
        private int Id;
        private int ReceivedIdsCounter;
        private int ReceivedReportsCounter;
        private State state;


        public ControlUnit(IConnection connection, string routingName, int n)
        {
            var factory = new ConnectionFactory(){ HostName = "localhost"};
            this._connection = factory.CreateConnection();
            this._neighbours = new List<NeighbourData>();
            this.RoutingName = routingName;
            this.ReceivedIdsCounter = 0;
            this.ReceivedReportsCounter = 0;
            this.state = State.WRIds;
            var nd = (double) n;
            this.Id = new Random().Next((int)(nd * nd * nd * nd));

        }

        public void AddNeighbour(string neighbourName)
        {
            var neighbour = new NeighbourData(neighbourName);
            _neighbours.Add(neighbour);
            _neighboursDict.Add(neighbourName, neighbour);
        }

        public void Control()
        {
            foreach (var neighbour in _neighbours)
                ListenForMessages(neighbour);
            // create channels for every Neighbour data
            // attach method to message received
            // create your own sender channel 
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
            switch (state)
            {
                case State.WRIds:
                    state = GatherIdMessages(message, key);
                    break;
                case State.Eval:
                    state = Evaluate(message, key);
                    break;
                case State.WNWHId:
                    state = HandleNeighbourReport(message, key);
                    break;
                case State.LWR:
                    state = WaitForNextRound(message, key);
                    break;
                case State.WP:
                    state = Play();
                    break;
            }
        }

        private State Play()
        {
            throw new NotImplementedException();
        }

        private State WaitForNextRound(string message, string senderRoutingName)
        {
            throw new NotImplementedException();
        }

        private State HandleNeighbourReport(string message, string senderRoutingName)
        {
            throw new NotImplementedException();
        }

        private State Evaluate(string message, string senderRoutingName)
        {
            throw new NotImplementedException();
        }

        private State GatherIdMessages(string message, string senderRoutingName)
        {
            State nextState = State.WRIds;
            if (int.TryParse(message, out int id))
            {
                nextState = HandleId(id, senderRoutingName);
            }
            else
            {
                Enum.TryParse<Message>(message, out Message messageType);
                switch (messageType)
                {
                    case Message.Lost:
                        nextState = 
                        break;
                    case Message.Finished:
                        break;
                    case Message.Won:
                        break;
                }
            }
        }

        private State HandleId(int id, string senderRoutingName)
        {
            throw new NotImplementedException();
        }
    }
}