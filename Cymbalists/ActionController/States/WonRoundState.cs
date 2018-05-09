using System.Collections.Generic;
using Cymbalists.ActionController.Transitions.TransitionDefinitions;

namespace Cymbalists.ActionController.States
{
    public class WonRoundState : ControlStateBase
    {
        public WonRoundState(List<TransitionDefinition> transitionDefinitions) : base(transitionDefinitions)
        {
        }
    }
}