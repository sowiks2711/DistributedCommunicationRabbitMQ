using System.Collections.Generic;
using Cymbalists.ActionController.Transitions;

namespace Cymbalists.ActionController.States
{
    public class WaitForRestResultsState : ControlStateBase
    {
        public WaitForRestResultsState(List<TransitionBase> transitions) : base(transitions)
        {
        }
    }
}