using System.Collections.Generic;
using System.IO;
using RabbitMQ.Client;

namespace Cymbalists
{
    internal class NodesFactory
    {
        private readonly IConnection _connection;

        public NodesFactory(IConnection connection)
        {
            this._connection = connection;
        }

        public List<Node> Create()
        {
            var nodes = new List<Node>();
            using (TextReader reader = File.OpenText(Program.PositionFilePath))
            {
                var n = int.Parse(reader.ReadLine());
                for (var i = 0; i < n; i++)
                {
                    var coordinates = reader.ReadLine().Split(' ');
                    nodes.Add(new Node(int.Parse(coordinates[0]), int.Parse(coordinates[1]), n, _connection, i.ToString()));
                }
            }

            return nodes;
        }
    }
}