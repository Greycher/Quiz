using QuizGame.Runtime.MenuSystem;
using UnityEngine;

namespace QuizGame.Runtime.StateMachine.States
{
    public abstract class GameStateWithMatchingMenu : GameState
    {
        [SerializeField] private Menu menu;
        
        public override void EnterState()
        {
            MenuManager.Instance.OpenMenu(menu);
        }

        public override void ExitState()
        {
            MenuManager.Instance.CloseMenu();
        }
    }
}