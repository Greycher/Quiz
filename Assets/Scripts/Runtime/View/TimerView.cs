using System;
using System.Collections;
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

        private Coroutine _coroutine;

        public void StartTimer(int seconds, Action onComplete)
        {
            Assert.IsTrue(seconds > 0);
            StopTimer();
            StartCoroutine(CountDownRoutine(seconds, onComplete));
        }
        
        public void StopTimer()
        {
            StopAllCoroutines();
        }

        private IEnumerator CountDownRoutine(float initialSeconds, Action onComplete)
        {
            var seconds = initialSeconds;
            UpdateTimer(initialSeconds, seconds);
            var interval = 1 / (float)fps;
            while (true)
            {
                yield return new WaitForSeconds(interval);
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