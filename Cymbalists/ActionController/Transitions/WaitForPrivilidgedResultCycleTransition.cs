using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    internal class WaitForPrivilidgedResultCycleTransition : TransitionBase
    {
        public WaitForPrivilidgedResultCycleTransition(NeighboursManager manager, ComunicationManager communicationManager, StatesRepository repo) : base(manager, communicationManager, repo)
        {
        }

        public override void TakeAction()
        {
        }

        public override bool ConditionSatisfied()
        {
            return Manager.HasPrivilidgedNeighbours(CommunicationManager.Id);
        }

        public override ControlStateBase GetTargetState()
        {
            return StatesRepo.WaitForPrivilidgedResultState;
        }
    }
}