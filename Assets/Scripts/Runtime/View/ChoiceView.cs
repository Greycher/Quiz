﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame.Runtime.View
{
    public class ChoiceView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private AnimatorStatePlayer correctAnswerAnimatorState;
        [SerializeField] private AnimatorStatePlayer wrongAnswerAnimatorState;
        [SerializeField] private AnimatorStatePlayer defaultAnimatorState;

        public bool Interactable
        {
            get => button.interactable;
            set => button.interactable = true;
        }

        public Action OnClick;
        
        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            OnClick?.Invoke();
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        public void SetChoiceText(string answer)
        {
            label.text = answer;
        }

        public IEnumerator AnimateCorrectAnswer()
        {
            yield return new WaitForSeconds(correctAnswerAnimatorState.Play());
        }

        public IEnumerator AnimateWrongAnswer()
        {
            yield return new WaitForSeconds(wrongAnswerAnimatorState.Play());
        }

        public void ResetAnimation()
        {
            defaultAnimatorState.Play();
        }
    }
}