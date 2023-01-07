using QuizGame.Runtime.Presenter;
using UnityEngine;

namespace QuizGame.Runtime.StateMachine.States
{
    public class QuizGameState : GameStateWithMatchingMenu
    {
        [SerializeField] private QuizPresenter quizPresenter;

        public override void EnterState()
        {
            base.EnterState();
            quizPresenter.StartQuiz();
        }

        public override void UpdateState() { }
    }
}