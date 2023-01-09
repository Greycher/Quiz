using System;
using System.Collections;
using Cysharp.Threading.Tasks;
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
            firstQuestionView.ExitScreenImmediate();
            secondQuestionView.ExitScreenImmediate();
            CurrentQuestionView = secondQuestionView;
            NextQuestionView = firstQuestionView;
        }

        private void OnAswerSelected(Answer answer)
        {
            OnAnswerSelect?.Invoke(answer);
        }

        public async UniTask SetNextQuestionAsync(Question question)
        {
            var temp = CurrentQuestionView;
            CurrentQuestionView = NextQuestionView;
            NextQuestionView = temp;
            
            CurrentQuestionView.SetQuestion(question);

            await UniTask.WhenAll(
                NextQuestionView.ExitScreenAsync(), 
                CurrentQuestionView.EnterScreenAsync());
        }
        
        public async UniTask AnimateSelectedAnswerAsync(Answer answer)
        {
            await CurrentQuestionView.AnimateSelectedAnswerAsync(answer);
        }
        
        public void VisualiseCorrectAnswer(Answer answer)
        {
            CurrentQuestionView.VisualiseCorrectAnswer(answer);
        }
        
        public void VisualiseWrongAnswer(Answer answer)
        {
            CurrentQuestionView.VisualiseWrongAnswer(answer);
        }
    }
}