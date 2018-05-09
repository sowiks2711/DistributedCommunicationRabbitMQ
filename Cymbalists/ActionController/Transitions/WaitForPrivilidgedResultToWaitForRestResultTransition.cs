using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class WaitForPrivilidgedResultToWaitForRestResultTransition:TransitionBase
    {
        public WaitForPrivilidgedResultToWaitForRestResultTransition(NeighboursManager manager) : base(manager)
        {
        }

        public override void TakeAction()
        {
            throw new System.NotImplementedException();
        }

        public override bool ConditionSatisfied()
        {
            throw new System.NotImplementedException();
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.WaitForRestResultsState;
        }
    }
}