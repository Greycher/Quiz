using System;
using UnityEngine;

namespace QuizGame.Runtime.Model
{
    [Serializable]
    public class LeaderboardPlayerData
    {
        [SerializeField] private int rank;
        [SerializeField] private string nickName;
        [SerializeField] private int score;

        public int Rank => rank;
        public string NickName => nickName;
        public int Score => score;

        public LeaderboardPlayerData(int rank, string nickname, int score)
        {
            this.rank = rank;
            nickName = nickname;
            this.score = score;
        }
    }
}