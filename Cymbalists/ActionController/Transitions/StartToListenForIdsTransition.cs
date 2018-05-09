using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Cymbalists.ActionController.States;
using Cymbalists.ActionController.Transitions.TransitionDefinitions;

namespace Cymbalists.ActionController.Transitions
{
    public class StartToListenForIdsTransition : TransitionBase
    {
        public StartToListenForIdsTransition(NeighboursManager manager, ControlStateBase from) : base(manager, from)
        {
        }

        public override void TakeAction()
        {
            ///
            /// TODO: send your id to neighbours
            ///
            Manager.BroadcastId();
            throw new System.NotImplementedException();
        }

        public override bool ConditionSatisfied()
        {
            return true;
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.ListenForIdsState;
        }
    }
}