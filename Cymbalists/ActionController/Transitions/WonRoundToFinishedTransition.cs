using System.Net.Sockets;
using System.Threading;
using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class WonRoundToFinishedTransition : TransitionBase
    {
        private readonly SemaphoreSlim _gate;
        public WonRoundToFinishedTransition(NeighboursManager manager, ComunicationManager communicationManager, StatesRepository repo, SemaphoreSlim gate) : base(manager, communicationManager, repo)
        {
            _gate = gate;
        }

        public override bool ConditionSatisfied()
        {
            return !CommunicationManager.FinishedAlreadySend;
        }
        public override void TakeAction()
        {
            //playing
            Thread.Sleep(Program.PlayingDuration);
            CommunicationManager.SendFinished();
            _gate.Release();
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.WonRoundState;
        }

    }
}