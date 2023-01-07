using UnityEngine;
using UnityEngine.UI;

namespace QuizGame.Runtime.MenuSystem.Menus
{
    public class LooseMenu : Menu
    {
        [SerializeField] private Button tryAgainBtn;
        [SerializeField] private Button mainMenuBtn;
        
        private void OnEnable()
        {
            tryAgainBtn.onClick.AddListener(OnTryAgainBtnClicked);
            mainMenuBtn.onClick.AddListener(OnMainMenuBtnClicked);
        }

        private void OnDisable()
        {
            tryAgainBtn.onClick.RemoveListener(OnTryAgainBtnClicked);
            mainMenuBtn.onClick.RemoveListener(OnMainMenuBtnClicked);
        }
        
        private void OnTryAgainBtnClicked()
        {
            SceneNavigator.LoadGameScene();
        }
        
        private void OnMainMenuBtnClicked()
        {
            SceneNavigator.LoadMainMenuScene();
        }
    }
}