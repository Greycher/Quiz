using System;
using System.Collections;
using Newtonsoft.Json;
using QuizGame.Runtime.Model;
using QuizGame.Runtime.Presenter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace QuizGame.Runtime
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button leaderboardButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private LeaderboardPresenter leaderboardPresenter;

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            leaderboardButton.onClick.AddListener(OnLeaderboardButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
        
        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClicked);
            leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonClicked);
            quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            SceneNavigator.LoadGameScene();
        }
        
        private void OnLeaderboardButtonClicked()
        {
            leaderboardPresenter.ShowLeaderboardPopup();
        }
        
        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}