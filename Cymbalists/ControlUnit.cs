using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    enum Message
    {
        Id,
        Lost,
        Won,
        Finished
    }
    public class ControlUnit
    {
        private int Id;
        private readonly NeighboursManager _manager;
        private ControlStateBase state;
        private readonly ComunicationManager _communicationManager;

        public ControlUnit(string routingName, NeighboursManager manager)
        {
            ///
            /// TODO: create state control machine and assign starting state
            //this.state = new StartingState();
            this._manager = manager;
            this.Id = new Random().Next(int.MaxValue);
            this._communicationManager = new Cymbalists.ComunicationManager(_manager, Id, routingName);

        }


        public void Control()
        {
            // create channels for every Neighbour data
            // attach method to message received
            // create your own sender channel 
            ///
            /// TODO: Move this to starting transition action
            SendYourId();
        }


        private void MakeNextMove(string message, string key)
        {
            _manager.UpdateNeighbourInfo(message, key);
            state = state.TakeAction();
        }
    }
}