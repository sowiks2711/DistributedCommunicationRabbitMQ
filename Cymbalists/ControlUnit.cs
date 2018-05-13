using System;
using System.Threading;
using Cymbalists.ActionController;
using Cymbalists.ActionController.States;

namespace Cymbalists
{
    public class ControlUnit : INegotiationController
    {
        private readonly ComunicationManager _communicationManager;
        private readonly NeighboursManager _neighboursManager;
        private ControlStateBase _state;

        public ControlUnit(string routingName, NeighboursManager neighboursManager)
        {
            _neighboursManager = neighboursManager;
            var id = new Random(int.Parse(routingName)).Next();
            //var id = int.Parse(routingName) // id as node nr
            _communicationManager = new ComunicationManager(this, id, routingName);
            var gate = new SemaphoreSlim(0, 1);
            var statesRepo = new StatesRepository(neighboursManager, _communicationManager, gate);
            _state = statesRepo.StartingState;
        }


        public void MakeNextMove(string message, string routingKey)
        {
            _neighboursManager.UpdateNeighbourInfo(message, routingKey);
            lock (_state)
            {
                _state = _state.TakeAction();
            }
        }


        public void Control()
        {
            lock (_state)
            {
                _state = _state.TakeAction();
            }
        }

        public void InitializeListening()
        {
            foreach (var neighbour in _neighboursManager.GetAll()) _communicationManager.ListenForMessages(neighbour);
        }
    }
}