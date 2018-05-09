using System.Collections.Generic;
using Cymbalists.ActionController.States;
using Cymbalists.ActionController.Transitions.TransitionDefinitions;

namespace Cymbalists.ActionController
{
    public class StatesRepository
    {
        public StatesRepository(NeighboursManager manager)
        {
            StartingState = new StartingState(
                new List<TransitionDefinition>
                {
                    new StartToListenForIdsDefinition(manager)
                });
            WonRoundState = new WonRoundState(new List<TransitionDefinition>());
            EvaluationState = new EvaluationState(
                new List<TransitionDefinition>
                {
                    new EvaluateToWaitForPrivilidgedResultDefinition(manager),
                    new EvaluateToWonRoundDefinition(manager)
                });
            WaitForPrivilidgedResultState = new WaitForPrivilidgedResultState(
                new List<TransitionDefinition>
                {
                    new WaitForPrivilidgedResultToEvaluateDefinition(manager),
                    new WaitForPrivilidgedResultToWaitForRestResultDefinition(manager)
                });
            WaitForRestResultsState = new WaitForRestResultsState(
                new List<TransitionDefinition>
                {
                    new WaitForRestResultToEvaluateDefinition(manager),
                    new WaitForRestResultCycleDefinition(manager)
                });
            ListenForIdsState = new ListenForIdsState(
                new List<TransitionDefinition>
                {
                    new ListenForIdsToEvaluateDefinition(manager),
                    new ListenForIdsCycleDefinition(manager)
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