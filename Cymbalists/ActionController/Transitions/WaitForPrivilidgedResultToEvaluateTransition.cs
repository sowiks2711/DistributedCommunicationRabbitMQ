﻿using System;
using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class WaitForPrivilidgedResultToEvaluateTransition : TransitionBase
    {
        public WaitForPrivilidgedResultToEvaluateTransition(NeighboursManager manager) : base(manager)
        {
        }

        public override void TakeAction()
        {
            throw new NotImplementedException();
        }

        public override bool ConditionSatisfied()
        {
            throw new NotImplementedException();
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.EvaluationState.TakeAction();
        }
    }
}