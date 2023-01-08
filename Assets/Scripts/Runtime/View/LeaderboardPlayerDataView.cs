using QuizGame.Runtime.Model;
using TMPro;
using UnityEngine;

namespace QuizGame.Runtime.View
{
    public class LeaderboardPlayerDataView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rankLabel;
        [SerializeField] private TextMeshProUGUI nickNameLabel;
        [SerializeField] private TextMeshProUGUI scoreLabel;
        
        public void SetLeaderboardPlayerData(LeaderboardPlayerData leaderboardPlayerData)
        {
            rankLabel.text = leaderboardPlayerData.Rank.ToString();
            nickNameLabel.text = leaderboardPlayerData.NickName;
            scoreLabel.text = leaderboardPlayerData.Score.ToString();
        }
    }
}