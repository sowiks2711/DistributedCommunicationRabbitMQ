using System;
using Cymbalists.ActionController.Transitions;
using System.Collections.Generic;
using Cymbalists.ActionController.Transitions.TransitionDefinitions;

namespace Cymbalists.ActionController.States
{
    public class ControlStateBase
    {
        private readonly List<TransitionBase> _transitions;

        protected ControlStateBase(List<TransitionDefinition> transitionDefinitions)
        {
            _transitions = new List<TransitionBase>();
            foreach (var transitionDefinition in transitionDefinitions)
            {
                _transitions.Add(transitionDefinition.BuildTransition(this));
            }
        }

        public ControlStateBase TakeAction()
        {
            foreach (var transition in _transitions)
            {
                if (transition.ConditionSatisfied())
                {
                    transition.TakeAction();
                    return transition.GetTargetState();
                }
            }
            throw new InvalidOperationException();
        }

    }
}