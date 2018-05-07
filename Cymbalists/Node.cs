namespace Cymbalists
{
    public class Node
    {
        public string RoutingName => _control.RoutingName;
        public Node(int x, int y, int n = 0, RabbitMQ.Client.IConnection connection = null, string routingName = "")
        {
            X = x;
            Y = y;
            _control = new ControlUnit(connection, routingName, n);
        }

        public int X { get; }
        public int Y { get; }

        public void AddNeighbour(string name)
        {
            _control.AddNeighbour(name);
        }

        private readonly ControlUnit _control;

        public void ControlMethod()
        {
            _control.Control();
        }
    }
}