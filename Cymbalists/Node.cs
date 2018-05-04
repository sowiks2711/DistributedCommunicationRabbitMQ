using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Cymbalists
{
    public class Node
    {

        public Node(int x, int y)
        {
            X = x;
            Y = y;
            Neighbours = new List<Node>();
        }

        public int X { get; }
        public int Y { get; }
        public List<Node> Neighbours { get; }

    }
}