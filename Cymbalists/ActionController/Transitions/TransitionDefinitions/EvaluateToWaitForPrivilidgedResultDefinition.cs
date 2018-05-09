using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions.TransitionDefinitions
{
    public class EvaluateToWaitForPrivilidgedResultDefinition : TransitionDefinition
    {
        public EvaluateToWaitForPrivilidgedResultDefinition(NeighboursManager manager) : base(manager)
        {
        }

        public override TransitionBase BuildTransition(ControlStateBase @from)
        {
            return new EvaluateToWaitForPrivilidgedResultTransition(Manager, from);
        }
    }
}