using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace QuizGame.Runtime.View
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerLabel;

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

        private IEnumerator CountDownRoutine(int seconds, Action onComplete)
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                SetLabel(--seconds);
                if (seconds <= 0)
                {
                    break;
                }
            }
            
            onComplete?.Invoke();
        }

        private void SetLabel(int seconds)
        {
            timerLabel.text = seconds.ToString("0");
        }
    }
}