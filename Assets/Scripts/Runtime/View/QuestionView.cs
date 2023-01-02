using System;
using System.Collections;
using DG.Tweening;
using EasyClap.Seneca.Attributes;
using QuizGame.Runtime.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame.Runtime.View
{
    public class QuestionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI questionLabel;
        [SerializeField] private Button choiceAButton;
        [SerializeField] private TextMeshProUGUI choiceALabel;
        [SerializeField] private Button choiceBButton;
        [SerializeField] private TextMeshProUGUI choiceBLabel;
        [SerializeField] private Button choiceCButton;
        [SerializeField] private TextMeshProUGUI choiceCLabel;
        [SerializeField] private Button choiceDButton;
        [SerializeField] private TextMeshProUGUI choiceDLabel;
        
        [Header("Animator")]
        [SerializeField] private Animator animator;
        [SerializeField, AnimatorParameter(AnimatorControllerParameterType.Float, nameof(animator))] 
        private int screenBlendParam;
        [SerializeField] private float outerLeftBlendValue = -1f;
        [SerializeField] private float outerRightBlendValue = 1f;

        public Action<Answer> OnAswerSelect;
        private bool _entered;
        private float _screenBlendValue;

        private bool Entered
        {
            get => _entered;
            set
            {
                if (value == _entered)
                {
                    return;
                }

                _entered = value;
                SetButtonsInteractable(_entered);
            }
        }

        private void Awake()
        {
            _screenBlendValue = (outerLeftBlendValue + outerRightBlendValue) / 2;
        }

        private void OnEnable()
        {
            choiceAButton.onClick.AddListener(() => OnAnswerSelected(Answer.A));
            choiceBButton.onClick.AddListener(() => OnAnswerSelected(Answer.B));
            choiceCButton.onClick.AddListener(() => OnAnswerSelected(Answer.C));
            choiceDButton.onClick.AddListener(() => OnAnswerSelected(Answer.D));
        }
        
        private void OnDisable()
        {
            choiceAButton.onClick.RemoveAllListeners();
            choiceBButton.onClick.RemoveAllListeners();
            choiceCButton.onClick.RemoveAllListeners();
            choiceDButton.onClick.RemoveAllListeners();
        }
        
        private void SetButtonsInteractable(bool entered)
        {
            choiceAButton.interactable = entered;
            choiceBButton.interactable = entered;
            choiceCButton.interactable = entered;
            choiceDButton.interactable = entered;
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
            choiceALabel.text = question.ChoiceA;
            choiceBLabel.text = question.ChoiceB;
            choiceCLabel.text = question.ChoiceC;
            choiceDLabel.text = question.ChoiceD;
        }

        public IEnumerator OuterLeftToScreenRoutine()
        {
            if (Entered)
            {
                yield break;
            }
            
            yield return DOTween.To(ScreenBlendSetter, outerLeftBlendValue, _screenBlendValue, 0.5f);
            Entered = true;
        }
        
        public IEnumerator ScreenToOuterRightRoutine()
        {
            if (!Entered)
            {
                yield break;
            }
            
            Entered = false;
            yield return DOTween.To(ScreenBlendSetter, _screenBlendValue, outerRightBlendValue, 0.5f);
        }

        public void SetToOuterLeft()
        {
            Entered = false;
            ScreenBlendSetter(outerLeftBlendValue);
        }
        
        public void SetToOuterRight()
        {
            Entered = false;
            ScreenBlendSetter(outerRightBlendValue);
        }
    }
}