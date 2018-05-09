using System.Collections.Generic;
using Cymbalists.ActionController.Transitions;
using Cymbalists.ActionController.Transitions.TransitionDefinitions;

namespace Cymbalists.ActionController.States
{
    public class StartingState : ControlStateBase
    {
        public StartingState(List<TransitionDefinition> transitionDefinitions) : base(transitionDefinitions)
        {
        }
    }
}