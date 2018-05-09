using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class WaitForRestResultToEvaluateTransition : TransitionBase
    {
        public WaitForRestResultToEvaluateTransition(NeighboursManager manager) : base(manager)
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
            return StatesRepo.EvaluationState.TakeAction();
        }
    }
}