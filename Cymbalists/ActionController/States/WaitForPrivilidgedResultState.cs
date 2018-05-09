using System.Collections.Generic;
using Cymbalists.ActionController.Transitions;

namespace Cymbalists.ActionController.States
{
    public class WaitForPrivilidgedResultState : ControlStateBase
    {
        public WaitForPrivilidgedResultState(List<TransitionBase> transitions) : base(transitions)
        {
        }
    }
}