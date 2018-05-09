using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions.TransitionDefinitions
{
    public class WaitForPrivilidgedResultToWaitForRestResultDefinition : TransitionDefinition
    {
        public WaitForPrivilidgedResultToWaitForRestResultDefinition(NeighboursManager manager) : base(manager)
        {
        }

        public override TransitionBase BuildTransition(ControlStateBase @from)
        {
            return new WaitForPrivilidgedResultToWaitForRestResultTransition(Manager, from);
        }
    }
}