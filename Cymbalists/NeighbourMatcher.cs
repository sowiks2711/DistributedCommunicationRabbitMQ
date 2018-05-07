using System;

namespace Cymbalists
{
    public class NeighbourMatcher
    {
        private readonly Node _that;

        public NeighbourMatcher(Node that)
        {
            _that = that;
        }
        public bool LowerBreakCondition( Node other)
        {
            var noMoreNeighboursBelow = _that.X - other.X > Program.HearingDistance;
            return noMoreNeighboursBelow;


        }
        public bool HigherBreakCondition(Node other)
        {
            var noMoreNeighboursAbove = other.X - _that.X > Program.HearingDistance;
            return noMoreNeighboursAbove;
        }

        public bool NeighbouringCondition(Node other)
        {
            var isNeighbour = false;
            double dx = _that.X - other.X;
            double dy = _that.Y - other.Y;
            if (Math.Sqrt(dx * dx + dy * dy) <= Program.HearingDistance)
                isNeighbour = true;
            return isNeighbour;
        }

    }
}