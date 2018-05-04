using System;
using System.Collections.Generic;

namespace Cymbalists
{
    public class GraphBuilder
    {
        private readonly List<Node> _nodes;

        public GraphBuilder(List<Node> nodes)
        {
            this._nodes = nodes;
        }

        public List<Node> Build()
        {
            _nodes.Sort((s,p) =>
            {
                var result = s.X.CompareTo(p.X);
                if (result == 0)
                    result = s.Y.CompareTo(p.Y);

                return result;
            });

            for (var i = 0; i < _nodes.Count; i++)
            {
                var node = _nodes[i];
                for (var j = i - 1; j >= 0; j--)
                {
                    if (LowerBreakCondition(node, _nodes[j]))
                        break;
                    if (NeighbouringCondition(node, _nodes[j]))
                        node.Neighbours.Add(_nodes[j]);
                }

                for (var j = i + 1; j < _nodes.Count; j++)
                {
                    if (HigherBreakCondition(node, _nodes[j]))
                        break;
                    if (NeighbouringCondition(node, _nodes[j]))
                        node.Neighbours.Add(_nodes[j]);

                }
            }
            return _nodes;
        }

        private static bool LowerBreakCondition(Node that, Node other)
        {
            var noMoreNeighboursBelow = that.X - other.X > Program.HearingDistance;
            return noMoreNeighboursBelow;


        }
        private static bool HigherBreakCondition(Node that, Node other)
        {
            var noMoreNeighboursAbove = other.X - that.X > Program.HearingDistance;
            return noMoreNeighboursAbove;
        }

        private static bool NeighbouringCondition(Node that, Node other)
        {
            var isNeighbour = false;
            double dx = that.X - other.X;
            double dy = that.Y - other.Y;
            if (Math.Sqrt(dx * dx + dy * dy) <= Program.HearingDistance)
                isNeighbour = true;
            return isNeighbour;
        }


    }
}