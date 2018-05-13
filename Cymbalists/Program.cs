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
    internal class Program
    {
        public static readonly string HostName = "localhost";
        public static readonly string PositionFilePath = "Resources/positions.txt";
        public static readonly string QueueName = "Queueu";
        public static readonly string ExchangeName = "cymbalists quarrel";
        public static readonly double HearingDistance = 3;
        public static readonly int PlayingDuration = 100;
        public static readonly Logger Logger = new Logger();

        public static void Main(string[] args)
        {
            var threads = new List<Thread>();

            var nodes = new NodesConnector(new NodesFactory().Create()).Connect();
            var units = new List<ControlUnit>();
            foreach (var node in nodes)
            {
                var controlUnit = node.CreateControlUnit();
                controlUnit.InitializeListening();
                units.Add(controlUnit);
            }

            foreach (var unit in units)
            {
                var thread = new Thread(unit.Control);
                thread.Start();
                threads.Add(thread);
            }

            foreach (var thread in threads)
                thread.Join();
        }
    }
}