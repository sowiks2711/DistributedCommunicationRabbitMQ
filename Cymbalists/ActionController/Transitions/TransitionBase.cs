using System.Threading;
using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public abstract class TransitionBase
    {
        protected readonly NeighboursManager Manager;
        protected readonly StatesRepository StatesRepo;
        protected readonly ComunicationManager CommunicationManager;

        protected TransitionBase(NeighboursManager manager, ComunicationManager communicationManager,
            StatesRepository repo)
        {
            Manager = manager;
            StatesRepo = repo;
            CommunicationManager = communicationManager;
        }

        public abstract void TakeAction();
        public abstract bool ConditionSatisfied();
        public abstract ControlStateBase GetTargetState();

        public void LogTransition()
        {
            Program.Logger.LoggTransition(CommunicationManager.Id, this.GetType().Name);
        }
    }
}