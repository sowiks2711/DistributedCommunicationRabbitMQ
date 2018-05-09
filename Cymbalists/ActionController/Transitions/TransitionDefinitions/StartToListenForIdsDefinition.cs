using System.Text.RegularExpressions;
using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions.TransitionDefinitions
{
    public class StartToListenForIdsDefinition : TransitionDefinition
    {
        public StartToListenForIdsDefinition(NeighboursManager manager) : base(manager)
        {
        }

        public override TransitionBase BuildTransition(ControlStateBase from)
        {
            return new StartToListenForIdsTransition(Manager, from);
        }
    }
}