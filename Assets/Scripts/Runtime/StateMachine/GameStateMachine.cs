using UnityEngine;

namespace QuizGame.Runtime.StateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        private GameState _currentGameState;

        private void Update()
        {
            UpdateState();
        }
        
        private void UpdateState()
        {
            _currentGameState.UpdateState();
        }

        public void TransitionToState(GameState newGameState)
        {
            if (_currentGameState)
            {
                _currentGameState.ExitState();
            }
            _currentGameState = newGameState;
            _currentGameState.EnterState();
        }
    }
}
