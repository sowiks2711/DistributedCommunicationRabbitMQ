using System;
using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class WaitForRestResultCycleTransition : TransitionBase
    {
        public WaitForRestResultCycleTransition(NeighboursManager manager, ComunicationManager communicationManager,
            StatesRepository repo) : base(manager, communicationManager, repo)
        {
        }

        public override void TakeAction()
        {
        }

        public override bool ConditionSatisfied()
        {
            return !Manager.ReadyForNextRound();
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.WaitForRestResultsState;
        }
    }
}