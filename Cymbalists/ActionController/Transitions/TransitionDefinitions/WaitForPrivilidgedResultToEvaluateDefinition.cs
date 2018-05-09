using System;
using System.Collections.Generic;
using System.Text;
using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions.TransitionDefinitions
{
    class WaitForPrivilidgedResultToEvaluateDefinition : TransitionDefinition
    {
        public WaitForPrivilidgedResultToEvaluateDefinition(NeighboursManager manager) : base(manager)
        {
        }

        public override TransitionBase BuildTransition(ControlStateBase @from)
        {
            return new WaitForRestResultToEvaluateTransition(Manager, from);
        }
    }
}
