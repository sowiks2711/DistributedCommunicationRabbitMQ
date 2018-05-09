using System.Collections.Generic;
using Cymbalists.ActionController.Transitions.TransitionDefinitions;

namespace Cymbalists.ActionController.States
{
    public class EvaluationState : ControlStateBase
    {
        public EvaluationState(List<TransitionDefinition> transitionDefinitions) : base(transitionDefinitions)
        {
        }
    }
}