using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public class EvaluateToWaitForPrivilidgedResultTransition : TransitionBase
    {
        public EvaluateToWaitForPrivilidgedResultTransition(NeighboursManager manager,
            ComunicationManager communicationManager, StatesRepository repo) : base(manager, communicationManager, repo)
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