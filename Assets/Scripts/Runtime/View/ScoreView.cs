using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace QuizGame.Runtime.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField, Multiline] private string format;
        [SerializeField] private float animationDuration = 0.5f;

        private int _score;
        private Tween _tween;

        public void SetScoreAnimated(int score)
        {
            if (_tween.IsActive() && _tween.IsPlaying())
            {
                _tween.Kill(true);
            }
            
            _tween = DOTween.To(GetScore, SetScore, score, animationDuration);
        }

        public void SetScore(int score)
        {
            _score = score;
            label.text = String.Format(format, _score);
        }
        
        private int GetScore()
        {
            return _score;
        }
    }
}