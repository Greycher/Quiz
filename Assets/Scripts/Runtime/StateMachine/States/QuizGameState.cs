using QuizGame.Runtime.MenuSystem;
using QuizGame.Runtime.Presenter;
using UnityEngine;

namespace QuizGame.Runtime.StateMachine.States
{
    public class QuizGameState : GameState
    {
        [SerializeField] private QuizPresenter quizPresenter;
        [SerializeField] private Menu menu;
        
        public override void EnterState()
        {
            MenuManager.Instance.OpenMenu(menu);
            quizPresenter.StartQuizSession();
        }

        public override void UpdateState() { }
        
        public override void ExitState()
        {
            MenuManager.Instance.CloseMenu();
        }
    }
}