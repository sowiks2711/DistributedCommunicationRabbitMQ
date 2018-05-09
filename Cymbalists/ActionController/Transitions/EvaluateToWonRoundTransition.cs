using System.Collections.Generic;
using Cymbalists.ActionController.States;
using Cymbalists.ActionController.Transitions.TransitionDefinitions;

namespace Cymbalists.ActionController.Transitions
{
    public class EvaluateToWonRoundTransition : TransitionBase
    {
        public EvaluateToWonRoundTransition(NeighboursManager manager) : base(manager)
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
            return StatesRepo.WonRoundState;
        }
    }
}