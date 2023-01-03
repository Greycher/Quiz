using System;
using System.Collections;
using DG.Tweening;
using QuizGame.Runtime.Model;
using TMPro;
using UnityEngine;

namespace QuizGame.Runtime.View
{
    public class QuestionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI questionLabel;
        [SerializeField] private ChoiceView choiceAView;
        [SerializeField] private ChoiceView choiceBView;
        [SerializeField] private ChoiceView choiceCView;
        [SerializeField] private ChoiceView choiceDView;

        [Header("Animator")]
        [SerializeField] private Animator animator;
        [SerializeField, AnimatorParameter(AnimatorControllerParameterType.Float, nameof(animator))] 
        private int screenBlendParam;
        [SerializeField] private float outerLeftBlendValue = -1f;
        [SerializeField] private float outerRightBlendValue = 1f;

        public Action<Answer> OnAswerSelect;
        private float _screenBlendValue;
        private bool _onScreen;
        
        private void Awake()
        {
            _screenBlendValue = (outerLeftBlendValue + outerRightBlendValue) / 2;
        }

        private void OnEnable()
        {
            choiceAView.OnClick += () => OnAnswerSelected(Answer.A);
            choiceBView.OnClick += () => OnAnswerSelected(Answer.B);
            choiceCView.OnClick += () => OnAnswerSelected(Answer.C);
            choiceDView.OnClick += () => OnAnswerSelected(Answer.D);
        }
        
        private void OnDisable()
        {
            choiceAView.OnClick = null;
            choiceBView.OnClick = null;
            choiceCView.OnClick = null;
            choiceDView.OnClick = null;
        }
        
        private void SetButtonsInteractable(bool interactable)
        {
            choiceAView.Interactable = interactable;
            choiceBView.Interactable = interactable;
            choiceCView.Interactable = interactable;
            choiceDView.Interactable = interactable;
        }

        private void OnAnswerSelected(Answer answer)
        {
            OnAswerSelect?.Invoke(answer);
        }

        private void ScreenBlendSetter(float value)
        {
            animator.SetFloat(screenBlendParam, value);
        }

        public void SetQuestionRoutine(Question question)
        {
            questionLabel.text = question.QuestionText;
            choiceAView.SetChoiceText(question.ChoiceA);
            choiceBView.SetChoiceText(question.ChoiceB);
            choiceCView.SetChoiceText(question.ChoiceC);
            choiceDView.SetChoiceText(question.ChoiceD);
            
            choiceAView.ResetAnimation();
            choiceBView.ResetAnimation();
            choiceCView.ResetAnimation();
            choiceDView.ResetAnimation();
        }

        public IEnumerator EnterScreen()
        {
            if (_onScreen)
            {
                yield break;
            }
            
            DOTween.To(ScreenBlendSetter, outerLeftBlendValue, _screenBlendValue, 0.5f);
            yield return new WaitForSeconds(0.5f);
            _onScreen = true;
            SetButtonsInteractable(true);
        }
        
        public IEnumerator LeaveScreen()
        {
            if (!_onScreen)
            {
                yield break;
            }
            
            SetButtonsInteractable(false);
            DOTween.To(ScreenBlendSetter, _screenBlendValue, outerRightBlendValue, 0.5f);
            yield return new WaitForSeconds(0.5f);
            _onScreen = false;
        }
        
        public void LeaveScreenImmediate()
        {
            _onScreen = false;
            SetButtonsInteractable(false);
            ScreenBlendSetter(outerRightBlendValue);
        }

        private ChoiceView GetToChoiceView(Answer answer)
        {
            switch (answer)
            {
                case Answer.A:
                    return choiceAView;
                case Answer.B:
                    return choiceBView;
                case Answer.C:
                    return choiceCView;
                case Answer.D:
                    return choiceDView;
            }

            return null;
        }

        public IEnumerator AnimateCorrectAnswer(Answer answer)
        {
            yield return GetToChoiceView(answer).AnimateCorrectAnswer();
        }

        public IEnumerator AnimateWrongAnswer(Answer answer)
        {
            yield return GetToChoiceView(answer).AnimateWrongAnswer();
        }
    }
}