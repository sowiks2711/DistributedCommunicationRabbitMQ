namespace Cymbalists
{
    public class NeighbourData
    {
        public string Name { get; }
        public int Id { get; set; }
        public bool? Won { get; set; }

        public NeighbourData(string name)
        {
            this.Name = name;
        }
    }
}