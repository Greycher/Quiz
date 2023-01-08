using ColorTilesHop.Runtime;
using ColorTilesHop.Runtime.Signals;
using QuizGame.Runtime.MenuSystem;
using QuizGame.Runtime.MenuSystem.Menus;
using UnityEngine;

namespace QuizGame.Runtime.StateMachine.States
{
    public class PostQuizGameState : GameState
    {
        [SerializeField] private QuizCompletedMenu quizCompletedMenu;

        private void Awake()
        {
            SignalBus<QuizSessionEndSignal>.AddListener(QuizSessionEndSignal);
        }
        
        private void QuizSessionEndSignal(QuizSessionEndSignal signal)
        {
            quizCompletedMenu.ScoreView.SetScore(signal.Score);
        }
        
        private void OnDestroy()
        {
            SignalBus<QuizSessionEndSignal>.RemoveListener(QuizSessionEndSignal);
        }

        public override void EnterState()
        {
            MenuManager.Instance.OpenMenu(quizCompletedMenu);
        }
        
        public override void UpdateState() { }

        public override void ExitState()
        {
            MenuManager.Instance.CloseMenu();
        }
    }
}