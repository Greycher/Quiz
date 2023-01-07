using System;
using System.Collections;
using Newtonsoft.Json;
using QuizGame.Runtime.Model;
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
        
        private GraphicRaycaster _raycaster;

        private void Awake()
        {
            _raycaster = GetComponent<GraphicRaycaster>();
        }

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
            StartCoroutine(FetchLeaderboard());
        }

        private IEnumerator FetchLeaderboard()
        {
            var eventSystem = EventSystem.current;
            eventSystem.enabled = false;
            UnityWebRequest request = UnityWebRequest.Get("localhost:8080/leaderboard?page=0");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                throw new Exception(request.error);
            }
            
            Debug.Log(request.downloadHandler.text);
            
            // try
            // {
            //     _quiz = JsonConvert.DeserializeObject<Quiz>(request.downloadHandler.text);
            //     Debug.Log("Quiz creation succeed.");
            // }
            // catch (JsonReaderException  e)
            // {
            //     throw new Exception($"Invalid character at position {e.LineNumber}, {e.LinePosition}");
            // }
            
            eventSystem.enabled = true;
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}