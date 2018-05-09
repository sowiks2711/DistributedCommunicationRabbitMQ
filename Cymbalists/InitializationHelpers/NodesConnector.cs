using System.Collections.Generic;

namespace Cymbalists.InitializationHelpers
{
    public class NodesConnector
    {
        private readonly List<Node> _nodes;

        public NodesConnector(List<Node> nodes)
        {
            this._nodes = nodes;
        }

        public List<Node> Connect()
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
                var matcher = new NeighbourMatcher(node);
                for (var j = i - 1; j >= 0; j--)
                {
                    if (matcher.LowerBreakCondition(_nodes[j]))
                        break;
                    if (matcher.NeighbouringCondition(_nodes[j]))
                        node.AddNeighbour(_nodes[j].RoutingName);
                }

                for (var j = i + 1; j < _nodes.Count; j++)
                {
                    if (matcher.HigherBreakCondition(_nodes[j]))
                        break;
                    if (matcher.NeighbouringCondition(_nodes[j]))
                        node.AddNeighbour(_nodes[j].RoutingName);

                }
            }
            return _nodes;
        }


    }
}