using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using QuizGame.Runtime.Model;
using QuizGame.Runtime.View;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;

namespace QuizGame.Runtime.Presenter
{
    public class LeaderboardPresenter : MonoBehaviour
    {
        [SerializeField] private LeaderboardPageView leaderboardPageView;

        private List<LeaderBoardPage> _leaderBoardPages = new List<LeaderBoardPage>();
        private bool _shown;

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
            Assert.IsTrue(pageIndex >= 0 && pageIndex < _leaderBoardPages.Count);
            leaderboardPageView.SetLeaderboardPage(_leaderBoardPages[pageIndex]);
        }
        
        private void OnCloseButtonClicked()
        {
            HideLeaderboardAsync();
        }

        private async void HideLeaderboardAsync()
        {
            await leaderboardPageView.OutroAnimationAsync();
            leaderboardPageView.gameObject.SetActive(false);
            _shown = false;
        }

        public async void ShowLeaderboardPopupAsync()
        {
            if (_shown)
            {
                return;
            }
            
            _shown = true;
            await FetchLeaderboardAsync();
            leaderboardPageView.gameObject.SetActive(true);
            leaderboardPageView.SetLeaderboardPage(_leaderBoardPages[0]);
            leaderboardPageView.EntryAnimationAsync();
        }

        private async UniTask FetchLeaderboardAsync()
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
                    await request.SendWebRequest();
                    
                    var result = request.result;
                    if (result == UnityWebRequest.Result.ConnectionError || 
                        result == UnityWebRequest.Result.ProtocolError || 
                        result == UnityWebRequest.Result.DataProcessingError)
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
        }
    }
}