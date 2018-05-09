using System.Collections.Generic;
using Cymbalists.ActionController.States;
using Cymbalists.ActionController.Transitions;

namespace Cymbalists.ActionController
{
    public class StatesRepository
    {
        public StatesRepository(NeighboursManager manager)
        {
            StartingState = new StartingState(
                new List<TransitionBase>
                {
                    new StartToListenForIdsTransition(manager)
                });
            WonRoundState = new WonRoundState(new List<TransitionBase>());
            EvaluationState = new EvaluationState(
                new List<TransitionBase>
                {
                    new EvaluateToWaitForPrivilidgedResultTransition(manager),
                    new EvaluateToWonRoundTransition(manager)
                });
            WaitForPrivilidgedResultState = new WaitForPrivilidgedResultState(
                new List<TransitionBase>
                {
                    new WaitForPrivilidgedResultToEvaluateTransition(manager),
                    new WaitForPrivilidgedResultToWaitForRestResultTransition(manager)
                });
            WaitForRestResultsState = new WaitForRestResultsState(
                new List<TransitionBase>
                {
                    new WaitForRestResultToEvaluateTransition(manager),
                    new WaitForRestResultCycleTransition(manager)
                });
            ListenForIdsState = new ListenForIdsState(
                new List<TransitionBase>
                {
                    new ListenForIdsToEvaluateTransition(manager),
                    new ListenForIdsCycleTransition(manager)
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