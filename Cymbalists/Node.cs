﻿namespace Cymbalists
{
    public class Node
    {
        private readonly NeighboursManager _manager;
        public string RoutingName;

        public Node(int x, int y, int n = 0, string routingName = "")
        {
            X = x;
            Y = y;
            _manager = new NeighboursManager();
            RoutingName = routingName;
        }

        public int X { get; }
        public int Y { get; }

        public void AddNeighbour(string name)
        {
            _manager.AddNeighbour(name);
        }

        public ControlUnit CreateControlUnit()
        {
            return new ControlUnit(RoutingName, _manager);
        }
    }
}