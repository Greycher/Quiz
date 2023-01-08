using QuizGame.Runtime.View;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame.Runtime.MenuSystem.Menus
{
    public class QuizCompletedMenu : Menu
    {
        [SerializeField] private ScoreView scoreView;
        [SerializeField] private Button playAgainBtn;
        [SerializeField] private Button mainMenuBtn;

        public ScoreView ScoreView => scoreView;

        private void OnEnable()
        {
            playAgainBtn.onClick.AddListener(OnPlayAgainBtnClicked);
            mainMenuBtn.onClick.AddListener(OnMainMenuBtnClicked);
        }

        private void OnDisable()
        {
            playAgainBtn.onClick.RemoveListener(OnPlayAgainBtnClicked);
            mainMenuBtn.onClick.RemoveListener(OnMainMenuBtnClicked);
        }
        
        private void OnPlayAgainBtnClicked()
        {
            SceneNavigator.LoadGameScene();
        }
        
        private void OnMainMenuBtnClicked()
        {
            SceneNavigator.LoadMainMenuScene();
        }
    }
}