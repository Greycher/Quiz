using System;
using ColorTilesHop.Runtime;
using ColorTilesHop.Runtime.Signals;
using QuizGame.Runtime.StateMachine;
using QuizGame.Runtime.StateMachine.States;
using UnityEngine;

namespace QuizGame.Runtime
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameStateMachine gameStateMachine;
        [SerializeField] private PreQuizGameState preQuizGameState;
        [SerializeField] private QuizGameState quizGameState;
        [SerializeField] private WinGameState winGameState;
        [SerializeField] private LooseGameState looseGameState;

        private void Awake()
        {
            gameStateMachine.TransitionToState(quizGameState);
            SignalBus<QuizSucceedSignal>.AddListener(OnQuizSucceed);
            SignalBus<QuizFailedSignal>.AddListener(OnQuizFailed);
        }

        private void OnDestroy()
        {
            SignalBus<QuizSucceedSignal>.RemoveListener(OnQuizSucceed);
            SignalBus<QuizFailedSignal>.RemoveListener(OnQuizFailed);
        }

        private void OnQuizSucceed(QuizSucceedSignal t)
        {
            gameStateMachine.TransitionToState(winGameState);
        }
        
        private void OnQuizFailed(QuizFailedSignal t)
        {
            gameStateMachine.TransitionToState(looseGameState);
        }
    }
}