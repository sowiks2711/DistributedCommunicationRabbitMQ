using System.Collections.Generic;
using Cymbalists.ActionController.Transitions;

namespace Cymbalists.ActionController.States
{
    public class StartingState : ControlStateBase
    {
        public StartingState(List<TransitionBase> transitions) : base(transitions)
        {
        }
    }
}