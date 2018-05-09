using System.Collections.Generic;
using Cymbalists.ActionController.Transitions.TransitionDefinitions;

namespace Cymbalists.ActionController.States
{
    public class WaitForRestResultsState : ControlStateBase
    {
        public WaitForRestResultsState(List<TransitionDefinition> transitionDefinitions) : base(transitionDefinitions)
        {
        }
    }
}