using System.Collections.Generic;
using Cymbalists.ActionController.Transitions;

namespace Cymbalists.ActionController.States
{
    public class ListenForIdsState : ControlStateBase
    {
        public ListenForIdsState(List<TransitionBase> transitions) : base(transitions)
        {
        }
    }
}