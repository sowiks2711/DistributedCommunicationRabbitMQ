using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions.TransitionDefinitions
{
    public abstract class TransitionDefinition
    {
        protected NeighboursManager Manager;
        public TransitionDefinition(NeighboursManager manager)
        {
            Manager = manager;
        }

        public abstract TransitionBase BuildTransition(ControlStateBase from);
    }
}