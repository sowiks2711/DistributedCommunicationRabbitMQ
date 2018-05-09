using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    internal class ListenForIdsCycleTransition : TransitionBase
    {
        public ListenForIdsCycleTransition(NeighboursManager manager, ControlStateBase from) : base(manager, from)
        {
        }

        public override void TakeAction()
        {
            throw new System.NotImplementedException();
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