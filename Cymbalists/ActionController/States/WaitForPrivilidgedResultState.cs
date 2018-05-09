using System.Collections.Generic;
using Cymbalists.ActionController.Transitions.TransitionDefinitions;

namespace Cymbalists.ActionController.States
{
    public class WaitForPrivilidgedResultState : ControlStateBase
    {
        public WaitForPrivilidgedResultState(List<TransitionDefinition> transitionDefinitions) : base(transitionDefinitions)
        {
        }
    }
}