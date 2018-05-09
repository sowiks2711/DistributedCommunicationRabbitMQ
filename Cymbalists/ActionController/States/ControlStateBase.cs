using System;
using System.Collections.Generic;
using Cymbalists.ActionController.Transitions;

namespace Cymbalists.ActionController.States
{
    public class ControlStateBase
    {
        private readonly List<TransitionBase> _transitions;

        protected ControlStateBase(List<TransitionBase> transitions)
        {
            _transitions = transitions;
        }

        public ControlStateBase TakeAction()
        {
            foreach (var transition in _transitions)
                if (transition.ConditionSatisfied())
                {
                    transition.TakeAction();
                    return transition.GetTargetState();
                }

            throw new InvalidOperationException();
        }
    }
}