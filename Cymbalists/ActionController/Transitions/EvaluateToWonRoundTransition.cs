using System;
using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class EvaluateToWonRoundTransition : TransitionBase
    {
        public EvaluateToWonRoundTransition(NeighboursManager manager, ComunicationManager communicationManager, StatesRepository repo) : base(manager, communicationManager, repo)
        {
        }

        public override void TakeAction()
        {
            CommunicationManager.SendWon();
        }

        public override bool ConditionSatisfied()
        {
            return Manager.HasWonRound(CommunicationManager.Id);
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.WonRoundState.TakeAction();
        }
    }
}