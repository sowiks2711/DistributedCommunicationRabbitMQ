using System.Collections.Generic;
using Cymbalists.ActionController.States;
using Cymbalists.ActionController.Transitions;

namespace Cymbalists.ActionController.Transitions
{
    public class EvaluateToWaitForPrivilidgedResultTransition : TransitionBase
    {
        public EvaluateToWaitForPrivilidgedResultTransition(NeighboursManager manager) : base(manager)
        {
        }

        public override void TakeAction()
        {
        }

        public override bool ConditionSatisfied()
        {
            return Manager.HasPrivilidgedNeighbours();
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.WaitForPrivilidgedResultState;
        }
    }
}