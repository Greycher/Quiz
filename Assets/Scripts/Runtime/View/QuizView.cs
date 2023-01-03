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
            firstQuestionView.LeaveScreenImmediate();
            secondQuestionView.LeaveScreenImmediate();
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
            
            //TODO make animation synchronous
            CurrentQuestionView.SetQuestionRoutine(question);
            yield return NextQuestionView.LeaveScreen();
            yield return CurrentQuestionView.EnterScreen();
        }

        public IEnumerator AnimateCorrectAnswer(Answer answer)
        {
            yield return CurrentQuestionView.AnimateCorrectAnswer(answer);
        }

        public IEnumerator AnimateWrongAnswer(Answer answer)
        {
            yield return CurrentQuestionView.AnimateWrongAnswer(answer);
        }
    }
}