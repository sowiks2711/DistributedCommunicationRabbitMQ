using System.Collections.Generic;
using Cymbalists.ActionController.Transitions;

namespace Cymbalists.ActionController.States
{
    public class EvaluationState : ControlStateBase
    {
        public EvaluationState(List<TransitionBase> transitions) : base(transitions)
        {
        }
    }
}