using System;
using TMPro;
using UnityEngine;

namespace QuizGame.Runtime.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private string format;

        public void UpdateScore(int score)
        {
            label.text = String.Format(format, score);
        }
    }
}