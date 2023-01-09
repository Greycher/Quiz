using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace QuizGame.Runtime.View
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerLabel;
        [SerializeField] private Image fillImage;
        [SerializeField] private int fps = 8;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        public void StartTimer(int seconds, Action onComplete)
        {
            Assert.IsTrue(seconds > 0);
            StopTimer();
            CountDownAsync(seconds, onComplete);
        }
        
        public void StopTimer()
        {
            _cts.Cancel();
        }

        private async UniTask CountDownAsync(float initialSeconds, Action onComplete)
        {
            var seconds = initialSeconds;
            UpdateTimer(initialSeconds, seconds);
            var interval = 1 / (float)fps;
            while (true)
            {
                _cts = new CancellationTokenSource();
                await UniTask.Delay(TimeSpan.FromSeconds(interval), 
                    DelayType.DeltaTime, PlayerLoopTiming.Update, _cts.Token);
                
                UpdateTimer(initialSeconds, seconds -= interval);
                if (seconds <= 0)
                {
                    break;
                }
            }
            
            onComplete?.Invoke();
        }

        private void UpdateTimer(float initialSeconds, float seconds)
        {
            timerLabel.text = seconds.ToString("0");
            fillImage.fillAmount = Mathf.Clamp01(seconds / initialSeconds);
        }
    }
}