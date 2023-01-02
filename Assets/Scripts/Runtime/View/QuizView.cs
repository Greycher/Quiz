using System;
using System.Collections;
using QuizGame.Runtime.Model;
using UnityEngine;

namespace QuizGame.Runtime.View
{
    public class QuizView : MonoBehaviour
    {
        [SerializeField] private QuestionView firstQuestionView;
        [SerializeField] private QuestionView secondQuestionView;

        public Action<Answer> OnAnswerSelect;
        private QuestionView _currentQuestionView;
        private QuestionView _nextQuestionView;

        private QuestionView CurrentQuestionView
        {
            get => _currentQuestionView;
            set
            {
                if (_currentQuestionView)
                {
                    _currentQuestionView.OnAswerSelect -= OnAswerSelected;
                }
                
                _currentQuestionView = value;
                _currentQuestionView.OnAswerSelect += OnAswerSelected;
            }
        }
        
        private QuestionView NextQuestionView
        {
            get => _nextQuestionView;
            set => _nextQuestionView = value;
        }

        private void Awake()
        {
            firstQuestionView.SetToOuterLeft();
            secondQuestionView.SetToOuterRight();
            CurrentQuestionView = secondQuestionView;
            NextQuestionView = firstQuestionView;
        }

        private void OnAswerSelected(Answer answer)
        {
            OnAnswerSelect?.Invoke(answer);
        }

        public IEnumerator SetNextQuestionRoutine(Question question)
        {
            var temp = CurrentQuestionView;
            CurrentQuestionView = NextQuestionView;
            NextQuestionView = temp;
            // (CurrentQuestionView, NextQuestionView) = (CurrentQuestionView, NextQuestionView);

            CurrentQuestionView.SetQuestionRoutine(question);
            yield return CurrentQuestionView.OuterLeftToScreenRoutine();
            yield return NextQuestionView.ScreenToOuterRightRoutine();

            // var a = CurrentQuestionView.OuterLeftToScreenRoutine();
            // var b = NextQuestionView.ScreenToOuterRightRoutine();
            // yield return a;
            // yield return b;
        }
    }
}