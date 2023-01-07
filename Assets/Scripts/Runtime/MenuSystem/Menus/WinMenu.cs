using System;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame.Runtime.MenuSystem.Menus
{
    public class WinMenu : Menu
    {
        [SerializeField] private Button playAgainBtn;
        [SerializeField] private Button mainMenuBtn;
        
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