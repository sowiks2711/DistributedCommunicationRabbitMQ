using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions.TransitionDefinitions
{
    public class WaitForRestResultToEvaluateDefinition : TransitionDefinition
    {
        public WaitForRestResultToEvaluateDefinition(NeighboursManager manager) : base(manager)
        {
        }

        public override TransitionBase BuildTransition(ControlStateBase @from)
        {
            return new WaitForRestResultToEvaluateTransition(Manager, from);
        }
    }
}