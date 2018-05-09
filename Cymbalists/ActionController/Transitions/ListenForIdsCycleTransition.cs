using System;
using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    internal class ListenForIdsCycleTransition : TransitionBase
    {
        public ListenForIdsCycleTransition(NeighboursManager manager) : base(manager)
        {
        }

        public override void TakeAction()
        {
            throw new NotImplementedException();
        }

        public override bool ConditionSatisfied()
        {
            return !Manager.ReceivedAllIds();
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.ListenForIdsState;
        }
    }
}