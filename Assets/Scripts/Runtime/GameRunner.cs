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
        [SerializeField] private QuizGameState quizGameState;
        [SerializeField] private PostQuizGameState postQuizGameState;

        private void Start()
        {
            gameStateMachine.TransitionToState(quizGameState);
            SignalBus<QuizSessionEndSignal>.AddListener(QuizSessionEndSignal);
        }

        private void OnDestroy()
        {
            SignalBus<QuizSessionEndSignal>.RemoveListener(QuizSessionEndSignal);
        }

        private void QuizSessionEndSignal(QuizSessionEndSignal t)
        {
            gameStateMachine.TransitionToState(postQuizGameState);
        }
    }
}