using System.Collections.Generic;
using Cymbalists.ActionController.States;
using Cymbalists.ActionController.Transitions;

namespace Cymbalists.ActionController
{
    public class StatesRepository
    {
        public StatesRepository(NeighboursManager manager, ComunicationManager communicationManager, System.Threading.SemaphoreSlim _gate)
        {
            StartingState = new StartingState(
                new List<TransitionBase>
                {
                    new StartToListenForIdsTransition(manager, communicationManager, this)
                });
            WonRoundState = new WonRoundState(transitions: new List<TransitionBase>{new WonRoundToFinishedTransition(manager, communicationManager, this, _gate), new FinishedCycleTransition(this)});
            EvaluationState = new EvaluationState(
                new List<TransitionBase>
                {
                    new EvaluateToWaitForPrivilidgedResultTransition(manager, communicationManager, this),
                    new EvaluateToWonRoundTransition(manager, communicationManager, this)
                });
            WaitForPrivilidgedResultState = new WaitForPrivilidgedResultState(
                new List<TransitionBase>
                {
                    new WaitForPrivilidgedResultToEvaluateTransition(manager, communicationManager, this),
                    new WaitForPrivilidgedResultToWaitForRestResultTransition(manager, communicationManager, this),
                    new WaitForPrivilidgedResultCycleTransition(manager, communicationManager, this)
                });
            WaitForRestResultsState = new WaitForRestResultsState(
                new List<TransitionBase>
                {
                    new WaitForRestResultToEvaluateTransition(manager, communicationManager, this),
                    new WaitForRestResultCycleTransition(manager, communicationManager, this)
                });
            ListenForIdsState = new ListenForIdsState(
                new List<TransitionBase>
                {
                    new ListenForIdsToEvaluateTransition(manager, communicationManager, this),
                    new ListenForIdsCycleTransition(manager, communicationManager, this)
                });
        }

        public ControlStateBase StartingState {get;}
        public ControlStateBase WonRoundState {get;}
        public ControlStateBase EvaluationState {get;}
        public ControlStateBase WaitForPrivilidgedResultState {get;}
        public ControlStateBase WaitForRestResultsState {get;}
        public ControlStateBase ListenForIdsState {get;}
    }
}