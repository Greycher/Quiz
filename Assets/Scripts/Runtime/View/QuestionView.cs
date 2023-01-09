using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using QuizGame.Runtime.Model;
using QuizGame.Runtime.SettingRegistry.Settings;
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
        
        private QuizSetting _quizSetting;
        private float _screenBlendValue;
        private bool _onScreen;

        private void Awake()
        {
            _quizSetting = SettingRegistry.SettingRegistry.Load<QuizSetting>();
            _screenBlendValue = (outerLeftBlendValue + outerRightBlendValue) / 2;
        }

        private void OnEnable()
        {
            choiceAView.OnClick.AddListener(() => OnAnswerSelected(Answer.A));
            choiceBView.OnClick.AddListener(() => OnAnswerSelected(Answer.B));
            choiceCView.OnClick.AddListener(() => OnAnswerSelected(Answer.C));
            choiceDView.OnClick.AddListener(() => OnAnswerSelected(Answer.D));
        }

        private void OnAnswerSelected(Answer answer)
        {
            OnAswerSelect?.Invoke(answer);
            SetButtonsInteractable(false);
        }

        private void ScreenBlendSetter(float value)
        {
            animator.SetFloat(screenBlendParam, value);
        }
        
        private void SetButtonsInteractable(bool interactable)
        {
            choiceAView.Interactable = interactable;
            choiceBView.Interactable = interactable;
            choiceCView.Interactable = interactable;
            choiceDView.Interactable = interactable;
        }

        public void SetQuestion(Question question)
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

        public async UniTask EnterScreenAsync()
        {
            if (_onScreen)
            {
                return;
            }
            
            var d = _quizSetting.QuestionEnterAnimDuration;
            DOTween.To(ScreenBlendSetter, outerLeftBlendValue, _screenBlendValue, d);
            await UniTask.Delay(TimeSpan.FromSeconds(d));
            _onScreen = true;
            SetButtonsInteractable(true);
        }
        
        public async UniTask ExitScreenAsync()
        {
            if (!_onScreen)
            {
                return;
            }
            
            SetButtonsInteractable(false);
            
            var d = _quizSetting.QuestionExitAnimDuration;
            DOTween.To(ScreenBlendSetter, _screenBlendValue, outerRightBlendValue, d);
            await UniTask.Delay(TimeSpan.FromSeconds(d));
            _onScreen = false;
        }
        
        public void ExitScreenImmediate()
        {
            _onScreen = false;
            SetButtonsInteractable(false);
            ScreenBlendSetter(outerRightBlendValue);
        }

        public async UniTask AnimateSelectedAnswerAsync(Answer answer)
        {
            await GetToChoiceView(answer).AnimateSelectedAnswerAsync();
        }
        
        public void VisualiseCorrectAnswer(Answer answer)
        {
            GetToChoiceView(answer).VisualiseCorrectAnswer();
        }
        
        public void VisualiseWrongAnswer(Answer answer)
        {
            GetToChoiceView(answer).VisualiseWrongAnswer();
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
    }
}