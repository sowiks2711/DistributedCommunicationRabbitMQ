using Cymbalists;
using Xunit;

namespace CymbalistsTests
{
    public class GraphBuilderTests
    {
        private readonly NeighbourMatcher _matcher;
        private readonly Node[] _neighbours;
        private readonly Node[] _nonNeighbours;


        public GraphBuilderTests()
        {
            this._matcher = new NeighbourMatcher(new Node(2, 2));
            _neighbours = new[]
            {
                new Node(1, 3),
                new Node(0, 1),
                new Node(5, 2),
                new Node(2, 5),
                new Node(0, 1),
            };
            _nonNeighbours = new[]
            {
                new Node(0, 0),
                new Node(4, 4),
                new Node(5, 2),
                new Node(3, 5),
                new Node(1, 5),
            };

        }
        [Theory]
        [InlineData(0,1)]
        [InlineData(1,3)]
        [InlineData(5,2)]
        [InlineData(2,5)]
        [InlineData(2,1)]
        [InlineData(3,2)]
        public void NeighbouringConditionFulfilledTest(int x, int y)
        {
            var other = new Node(x, y);
            Assert.True(_matcher.NeighbouringCondition(other));
        }
        [Theory]
        [InlineData(-1,0)]
        [InlineData(0,5)]
        [InlineData(0,-1)]
        [InlineData(1,5)]
        [InlineData(3,-1)]
        [InlineData(4,5)]
        public void NeighbouringConditionUnfulfilledTest(int x, int y)
        {
            var other = new Node(x, y);
            Assert.False(_matcher.NeighbouringCondition(other));
        }
        [Theory]
        [InlineData(-2, 0)]
        [InlineData(-3, 5)]
        [InlineData(-3,-1)]
        [InlineData(-1, 0)]
        [InlineData( 0, 5)]
        [InlineData( 0,-1)]
        [InlineData( 1, 7)]
        [InlineData( 3,-1)]
        [InlineData( 4, 5)]
        public void HigherBreakConditionUnfulfilledTest(int x, int y)
        {
            var other = new Node(x, y);
            Assert.False(_matcher.HigherBreakCondition(other));
        }
        [Theory]
        [InlineData( 6, 5)]
        [InlineData( 7,-1)]
        [InlineData( 8, 5)]
        public void HigherBreakConditionFulfilledTest(int x, int y)
        {
            var other = new Node(x, y);
            Assert.True(_matcher.HigherBreakCondition(other));
        }
        [Theory]
        [InlineData( 6, 5)]
        [InlineData( 7,-1)]
        [InlineData( 8, 5)]
        [InlineData(-1, 0)]
        [InlineData( 0, 5)]
        [InlineData( 0,-1)]
        [InlineData( 1, 7)]
        [InlineData( 3,-1)]
        [InlineData( 4, 5)]
        public void LowerBreakConditionUnfulfilledTest(int x, int y)
        {
            var other = new Node(x, y);
            Assert.False(_matcher.LowerBreakCondition(other));
        }
        [Theory]
        [InlineData(-2, 0)]
        [InlineData(-3, 5)]
        [InlineData(-3,-1)]
        public void LowerBreakConditionFulfilledTest(int x, int y)
        {
            var other = new Node(x, y);
            Assert.True(_matcher.LowerBreakCondition(other));
        }
    }
}
