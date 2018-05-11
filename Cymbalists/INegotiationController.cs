namespace Cymbalists
{
    public interface INegotiationController
    {
        void MakeNextMove(string message, string routingKey);
    }
}