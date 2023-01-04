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
            while (true)
            {
                yield return new WaitForSeconds(0.25f);
                UpdateTimer(initialSeconds, seconds -= 0.125f);
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