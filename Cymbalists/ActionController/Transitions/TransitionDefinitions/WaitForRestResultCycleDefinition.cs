using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions.TransitionDefinitions
{
    public class WaitForRestResultCycleDefinition : TransitionDefinition
    {
        public WaitForRestResultCycleDefinition(NeighboursManager manager) : base(manager)
        {
        }

        public override TransitionBase BuildTransition(ControlStateBase @from)
        {
            return new WaitForRestResultCycleTransition(Manager, from);
        }
    }
}