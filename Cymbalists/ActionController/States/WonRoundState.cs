using System.Collections.Generic;
using Cymbalists.ActionController.Transitions;

namespace Cymbalists.ActionController.States
{
    public class WonRoundState : ControlStateBase
    {
        public WonRoundState(List<TransitionBase> transitions) : base(transitions)
        {
        }
    }
}