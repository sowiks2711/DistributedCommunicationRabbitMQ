using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Cymbalists.ActionController;
using Cymbalists.ActionController.States;
using RabbitMQ.Client.Framing.Impl;

namespace Cymbalists
{
    //enum State
    //{
    //    WRIds,
    //    Eval,
    //    WNWHId,
    //    LWR,
    //    WP
    //}

    internal enum Message
    {
        Id,
        Lost,
        Won,
        Finished
    }

    public class ControlUnit : INegotiationController
    {
        private readonly NeighboursManager _manager;
        private ControlStateBase state;
        private readonly ComunicationManager _communicationManager;
        private readonly SemaphoreSlim _gate;

        public ControlUnit(string routingName, NeighboursManager manager)
        {
            ///
            /// TODO: create state control machine and assign starting state
            /// this.state = new StartingState();
            this._manager = manager;
            this._gate = new SemaphoreSlim(0, 1);
            //var id = new Random().Next(int.MaxValue);
            var id = int.Parse(routingName);
            this._communicationManager = new Cymbalists.ComunicationManager(this, id, routingName);
            var statesRepo = new StatesRepository(manager, _communicationManager, _gate);
            this.state = statesRepo.StartingState;
        }


        public void Control()
        {
            lock (state)
            {
                state = state.TakeAction();
            }
        }


        public void MakeNextMove(string message, string routingKey)
        {
            _manager.UpdateNeighbourInfo(message, routingKey);
            lock (state)
            {
                state = state.TakeAction();
            }
        }

        public void InitializeListening()
        {
            foreach (var neighbour in _manager.GetAll())
            {
                _communicationManager.ListenForMessages(neighbour);
            }
        }
    }
}