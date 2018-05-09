using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Cymbalists
{
    public class NeighboursManager
    {
        public NeighboursManager()
        {
            _receivedIdsCounter = 0;
            _receivedReportsCounter = 0;
            _neighbours = new List<NeighbourData>();
            _neighboursDict = new Dictionary<string, NeighbourData>();
        }
        private readonly List<NeighbourData> _neighbours;
        private readonly Dictionary<string, NeighbourData> _neighboursDict;

        internal bool HasPrivilidgedNeighbours()
        {
            throw new NotImplementedException();
        }

        private int _receivedIdsCounter;
        private int _receivedReportsCounter;
        public void AddNeighbour(string neighbourName)
        {
            var neighbour = new NeighbourData(neighbourName);
            _neighbours.Add(neighbour);
            _neighboursDict.Add(neighbourName, neighbour);
        }

        public IEnumerable<NeighbourData> GetAll()
        {
            return _neighbours;
        }

        public void UpdateNeighbourInfo(string message, string key)
        {
            ///
            /// TODO: Handle different types of messages here
            /// 

            throw new NotImplementedException();
            
        }

        public bool ReceivedAllIds()
        {
            ///
            /// TODO: Check if all neighbours have id attached
            ///
            throw new NotImplementedException();
        }

        public void BroadcastId()
        {
            throw new NotImplementedException();
        }
    }
}