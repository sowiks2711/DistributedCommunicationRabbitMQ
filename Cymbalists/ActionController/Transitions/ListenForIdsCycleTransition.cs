using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    internal class ListenForIdsCycleTransition : TransitionBase
    {
        public ListenForIdsCycleTransition(NeighboursManager manager, ComunicationManager communicationManager,
            StatesRepository repo) : base(manager, communicationManager, repo)
        {
        }

        public override void TakeAction()
        {
        }

        public override bool ConditionSatisfied()
        {
            return !Manager.ReceivedAllIds();
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.ListenForIdsState;
        }
    }
}