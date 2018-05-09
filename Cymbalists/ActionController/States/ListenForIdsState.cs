using System.Collections.Generic;
using Cymbalists.ActionController.Transitions;
using Cymbalists.ActionController.Transitions.TransitionDefinitions;

namespace Cymbalists.ActionController.States
{
    public class ListenForIdsState : ControlStateBase
    {
        public ListenForIdsState(List<TransitionDefinition> transitionDefinitions) : base(transitionDefinitions)
        {
        }
    }
}