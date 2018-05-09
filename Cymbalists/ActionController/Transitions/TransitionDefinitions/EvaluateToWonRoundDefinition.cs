using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions.TransitionDefinitions
{
    public class EvaluateToWonRoundDefinition: TransitionDefinition
    {
        public EvaluateToWonRoundDefinition(NeighboursManager manager) : base(manager)
        {
        }

        public override TransitionBase BuildTransition(ControlStateBase @from)
        {
            return new EvaluateToWonRoundTransition(Manager, from);
        }
    }
}