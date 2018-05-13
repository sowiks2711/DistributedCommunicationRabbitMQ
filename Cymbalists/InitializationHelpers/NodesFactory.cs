using System.Collections.Generic;
using System.IO;

namespace Cymbalists.InitializationHelpers
{
    internal class NodesFactory
    {
        public List<Node> Create()
        {
            var nodes = new List<Node>();
            using (TextReader reader = File.OpenText(Program.PositionFilePath))
            {
                var n = int.Parse(reader.ReadLine());
                for (var i = 0; i < n; i++)
                {
                    var coordinates = reader.ReadLine().Split(' ');
                    nodes.Add(new Node(int.Parse(coordinates[0]), int.Parse(coordinates[1]), n, i.ToString()));
                }
            }

            return nodes;
        }
    }
}