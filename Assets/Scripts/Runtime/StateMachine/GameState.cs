using UnityEngine;

namespace QuizGame.Runtime.StateMachine
{
    public abstract class GameState : MonoBehaviour
    {
        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
    }
}