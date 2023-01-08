using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using QuizGame.Runtime.Model;
using QuizGame.Runtime.View;
using UnityEngine;
using UnityEngine.Networking;

namespace QuizGame.Runtime.Presenter
{
    public class LeaderboardPresenter : MonoBehaviour
    {
        [SerializeField] private LeaderboardPageView leaderboardPageView;

        private List<LeaderBoardPage> _leaderBoardPages = new List<LeaderBoardPage>();

        private void OnEnable()
        {
            leaderboardPageView.OnPageIndexChange += OnPageIndexChanged;
            leaderboardPageView.OnCloseButtonClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            leaderboardPageView.OnPageIndexChange -= OnPageIndexChanged;
            leaderboardPageView.OnCloseButtonClick.RemoveListener(OnCloseButtonClicked);
        }
        
        private void OnPageIndexChanged(int pageIndex)
        {
            leaderboardPageView.SetLeaderboardPage(_leaderBoardPages[pageIndex]);
        }
        
        private void OnCloseButtonClicked()
        {
            leaderboardPageView.gameObject.SetActive(false);
        }

        public void ShowLeaderboardPopup()
        {
            StartCoroutine(ShowLeaderboardRoutine());
        }
        
        private IEnumerator ShowLeaderboardRoutine()
        {
            _leaderBoardPages.Clear();
            var pageIndex = 0;
            var limit = 100;
            while (true)
            {
                if (--limit == 0)
                {
                    throw new Exception("Possible infinite loop!");
                }
                
                using (UnityWebRequest request = UnityWebRequest.Get($"localhost:8080/leaderboard?page={pageIndex++}"))
                {
                    yield return request.SendWebRequest();
                    
                    if (request.isNetworkError || request.isHttpError)
                    {
                        throw new Exception(request.error);
                    }
                    
                    try
                    {
                        var page = JsonConvert.DeserializeObject<LeaderBoardPage>(request.downloadHandler.text);
                        _leaderBoardPages.Add(page);
                        if (page.IsLast)
                        {
                            break;
                        }
                    }
                    catch (JsonReaderException  e)
                    {
                        throw new Exception($"Invalid character at position {e.LineNumber}, {e.LinePosition}");
                    }
                }
            }
            
            leaderboardPageView.gameObject.SetActive(true);
            leaderboardPageView.SetLeaderboardPage(_leaderBoardPages[0]);
        }
    }
}