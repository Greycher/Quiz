using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuizGame.Runtime.Model
{
    [Serializable]
    public class LeaderBoardPage
    {
        [SerializeField] private int pageIndex;
        [SerializeField] private bool isLast;
        [SerializeField] private List<LeaderboardPlayerData> data;

        public int PageIndex => pageIndex;
        public bool IsLast => isLast;
        public List<LeaderboardPlayerData> Data => data;

        public LeaderBoardPage(int page, bool is_last, List<LeaderboardPlayerData> data)
        {
            pageIndex = page;
            isLast = is_last;
            this.data = data;
        }
    }
}