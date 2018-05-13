using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class StartToListenForIdsTransition : TransitionBase
    {
        public StartToListenForIdsTransition(NeighboursManager manager, ComunicationManager communicationManager,
            StatesRepository repo) : base(manager, communicationManager, repo)
        {
        }

        public override void TakeAction()
        {
            CommunicationManager.SendId();
        }

        public override bool ConditionSatisfied()
        {
            return true;
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.ListenForIdsState;
        }
    }
}