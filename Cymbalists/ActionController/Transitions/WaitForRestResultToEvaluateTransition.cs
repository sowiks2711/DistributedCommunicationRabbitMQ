using System;
using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class WaitForRestResultToEvaluateTransition : TransitionBase
    {
        public WaitForRestResultToEvaluateTransition(NeighboursManager manager, ComunicationManager communicationManager, StatesRepository repo) : base(manager, communicationManager, repo)
        {
        }

        public override void TakeAction()
        {
            Manager.InitializeNextRound();
        }

        public override bool ConditionSatisfied()
        {
            return Manager.ReadyForNextRound();
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.EvaluationState.TakeAction();
        }
    }
}