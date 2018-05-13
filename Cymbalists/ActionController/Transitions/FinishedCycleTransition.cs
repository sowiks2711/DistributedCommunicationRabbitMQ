using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    internal class FinishedCycleTransition : TransitionBase
    {
        public FinishedCycleTransition(StatesRepository statesRepository): base(null, null, statesRepository)
        {
        }

        public override void TakeAction()
        {
        }

        public override bool ConditionSatisfied()
        {
            return true;
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.WonRoundState;
        }
    }
}