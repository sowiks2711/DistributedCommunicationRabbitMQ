using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Cymbalists
{
    public class NeighboursManager
    {
        public NeighboursManager()
        {
            _receivedIdsCounter = 0;
            _looserNeighboursCounter = 0;
            _receivedWonMessages = 0;
            _neighbours = new List<NeighbourData>();
            _neighboursDict = new Dictionary<string, NeighbourData>();
        }
        private readonly List<NeighbourData> _neighbours;
        private readonly Dictionary<string, NeighbourData> _neighboursDict;
        private int _receivedWonMessages;
        private int _receivedIdsCounter;
        private int _looserNeighboursCounter;

        internal bool HasPrivilidgedNeighbours(int id)
        {
            return _neighbours.Any(n => n.Id > id && n.Won != false);
        }

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
            if (int.TryParse(message, out int id))
            {
                AssignIdToNeighbour(id, key);
            }
            else
            {
                Enum.TryParse(message, out Message messageType);
                HandleMessageType(messageType, key);
            }
        }

        private void HandleMessageType(Message messageType, string key)
        {
            switch (messageType)
            {
                case Message.Finished:
                    RemoveFromNeighbours(key);
                    break;
                case Message.Lost:
                    _looserNeighboursCounter++;
                    MarkAsLost(key);
                    break;
                case Message.Won:
                    MarkAsWon(key);
                    break;
                default:
                    Console.WriteLine("Not supported message type.");
                    break;
            }
        }

        private void MarkAsWon(string key)
        {
            _receivedWonMessages++;
            _neighboursDict[key].Won = true;
        }

        private void MarkAsLost(string key)
        {
            _neighboursDict[key].Won = false;
        }

        private void RemoveFromNeighbours(string key)
        {
            var neighbourToRemove = _neighboursDict[key];
            _neighbours.Remove(neighbourToRemove);
            _neighboursDict.Remove(key);
        }

        private void AssignIdToNeighbour(int id, string key)
        {
            _neighboursDict[key].Id = id;
            _receivedIdsCounter++;
        }

        public bool ReceivedAllIds()
        {
            return _receivedIdsCounter == _neighbours.Count;
        }

        public bool HasWonRound(int id)
        {
            return _neighbours.All(n => n.Id < id || n.Won == false);
        }

        public bool HasNeighbourWon()
        {
            return _receivedWonMessages > 0;
        }

        public bool ReadyForNextRound()
        {
            return _neighbours.Count == _looserNeighboursCounter;
        }

        public void InitializeNextRound()
        {
            _receivedWonMessages = 0;
            _looserNeighboursCounter = 0;
            _neighbours.ForEach(n => n.Won = null);
        }
    }
}