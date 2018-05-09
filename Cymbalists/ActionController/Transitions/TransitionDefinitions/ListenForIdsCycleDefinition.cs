using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions.TransitionDefinitions
{
    internal class ListenForIdsCycleDefinition : TransitionDefinition
    {
        public ListenForIdsCycleDefinition(NeighboursManager manager) : base(manager)
        {
        }

        public override TransitionBase BuildTransition(ControlStateBase @from)
        {
            return new ListenForIdsCycleTransition(Manager, from);
        }
    }
}