using Cymbalists.ActionController.States;

namespace Cymbalists.ActionController.Transitions
{
    public abstract class TransitionBase
    {
        protected NeighboursManager Manager;
        protected StatesRepository StatesRepo;

        protected TransitionBase(NeighboursManager manager)
        {
            Manager = manager;
            StatesRepo = new StatesRepository(manager);
        }

        public abstract void TakeAction();
        public abstract bool ConditionSatisfied();
        public abstract ControlStateBase GetTargetState();
    }
}