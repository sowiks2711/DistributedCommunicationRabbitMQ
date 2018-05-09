using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions.TransitionDefinitions
{
    internal class ListenForIdsToEvaluateDefinition : TransitionDefinition
    {
        public ListenForIdsToEvaluateDefinition(NeighboursManager manager) : base(manager)
        {
        }

        public override TransitionBase BuildTransition(ControlStateBase @from)
        {
            return new ListenForIdsToEvaluateTransition(Manager, from);
        }
    }
}