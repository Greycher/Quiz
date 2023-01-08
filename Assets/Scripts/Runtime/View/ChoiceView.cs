﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame.Runtime.View
{
    public class ChoiceView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private AnimatorStatePlayer selectedAnswerAnimation;
        [SerializeField] private AnimatorStatePlayer correctAnswerAnimatorState;
        [SerializeField] private AnimatorStatePlayer wrongAnswerAnimatorState;
        [SerializeField] private AnimatorStatePlayer defaultAnimatorState;

        public bool Interactable
        {
            get => button.interactable;
            set => button.interactable = value;
        }

        public Button.ButtonClickedEvent OnClick
        {
            get => button.onClick;
            set => button.onClick = value;
        }
        
        public void SetChoiceText(string answer)
        {
            label.text = answer;
        }

        public IEnumerator AnimateSelectedAnswer()
        {
            yield return new WaitForSeconds(selectedAnswerAnimation.Play());
        }

        public void VisualiseCorrectAnswer()
        {
            correctAnswerAnimatorState.Play();
        }
        
        public void VisualiseWrongAnswer()
        {
            wrongAnswerAnimatorState.Play();
        }

        public void ResetAnimation()
        {
            defaultAnimatorState.Play();
        }
    }
}