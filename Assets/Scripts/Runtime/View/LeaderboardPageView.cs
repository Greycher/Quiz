using System;
using QuizGame.Runtime.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace QuizGame.Runtime.View
{
    public class LeaderboardPageView : MonoBehaviour
    {
        [SerializeField] private LeaderboardPlayerDataView[] leaderboardPlayerDataView = new LeaderboardPlayerDataView[10];
        [SerializeField] private Button closeButton;
        [SerializeField] private Button prevPageButton;
        [SerializeField] private Button nextPageButton;
        [SerializeField] private TextMeshProUGUI pageNumberLabel;

        public Action<int> OnPageIndexChange;

        public Button.ButtonClickedEvent OnCloseButtonClick
        {
            get => closeButton.onClick;
            set => closeButton.onClick = value;
        }
        
        private LeaderBoardPage _page;

        private void OnEnable()
        {
            prevPageButton.onClick.AddListener(OnPrevPageButtonClicked);
            nextPageButton.onClick.AddListener(OnNextPageButtonClicked);
        }
        
        private void OnDisable()
        {
            prevPageButton.onClick.RemoveListener(OnPrevPageButtonClicked);
            nextPageButton.onClick.RemoveListener(OnNextPageButtonClicked);
        }

        private void OnPrevPageButtonClicked()
        {
            Assert.IsTrue(_page.PageIndex > 0);
            OnPageIndexChange.Invoke(_page.PageIndex - 1);
        }
        
        private void OnNextPageButtonClicked()
        {
            Assert.IsFalse(_page.IsLast);
            OnPageIndexChange.Invoke(_page.PageIndex + 1);
        }
        
        public void SetLeaderboardPage(LeaderBoardPage page)
        {
            _page = page;
            var data = _page.Data;
            Assert.IsTrue(leaderboardPlayerDataView.Length == data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                leaderboardPlayerDataView[i].SetLeaderboardPlayerData(data[i]);
            }

            pageNumberLabel.text = (_page.PageIndex + 1).ToString();
            prevPageButton.gameObject.SetActive(_page.PageIndex > 0);
            nextPageButton.gameObject.SetActive(!_page.IsLast);
        }
    }
}