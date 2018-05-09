using System;
using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class StartToListenForIdsTransition : TransitionBase
    {
        public StartToListenForIdsTransition(NeighboursManager manager) : base(manager)
        {
        }

        public override void TakeAction()
        {
            ///
            /// TODO: send your id to neighbours
            ///
            Manager.BroadcastId();
            throw new NotImplementedException();
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