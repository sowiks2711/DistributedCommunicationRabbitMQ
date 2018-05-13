using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class WaitForPrivilidgedResultToWaitForRestResultTransition : TransitionBase
    {
        public WaitForPrivilidgedResultToWaitForRestResultTransition(NeighboursManager manager,
            ComunicationManager communicationManager, StatesRepository repo) : base(manager, communicationManager, repo)
        {
        }

        public override void TakeAction()
        {
            CommunicationManager.SendLost();
        }

        public override bool ConditionSatisfied()
        {
            return Manager.HasNeighbourWon();
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.WaitForRestResultsState;
        }
    }
}