using System;
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
        [SerializeField] private Animator animator;
        [SerializeField, AnimatorState(nameof(animator))] private int correctAnswerAnimatorState;
        [SerializeField, AnimatorState(nameof(animator))] private int wrongAnswerAnimatorState;
        [SerializeField, AnimatorState(nameof(animator))] private int defaultAnimatorState;

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
            animator.Play(correctAnswerAnimatorState);
            animator.Update(0);
            Debug.Log($"length: {animator.GetCurrentAnimatorStateInfo(0).length}");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        public IEnumerator AnimateWrongAnswer()
        {
            animator.Play(wrongAnswerAnimatorState);
            animator.Update(0);
            Debug.Log($"length: {animator.GetCurrentAnimatorStateInfo(0).length}");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        public void ResetAnimation()
        {
            animator.Play(defaultAnimatorState);
        }
    }
}