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

        public Action<Answer> OnAsnwerSelect;
        private QuestionView _currentQuestionView;
        private QuestionView _nextQuestionView;

        private void Awake()
        {
            firstQuestionView.SetToOuterLeft();
            secondQuestionView.SetToOuterRight();
            _currentQuestionView = secondQuestionView;
            _nextQuestionView = firstQuestionView;
        }

        private void OnEnable()
        {
            firstQuestionView.OnAswerSelect += OnAsnwerSelect;
            secondQuestionView.OnAswerSelect += OnAsnwerSelect;
        }

        private void OnDisable()
        {
            firstQuestionView.OnAswerSelect -= OnAsnwerSelect;
            secondQuestionView.OnAswerSelect -= OnAsnwerSelect;
        }

        public IEnumerator SetNextQuestionRoutine(Question question)
        {
            _nextQuestionView.SetQuestionRoutine(question);
            var a = _currentQuestionView.ScreenToOuterRightRoutine();
            var b = _nextQuestionView.OuterLeftToScreenRoutine();
            yield return a;
            yield return b;
            
            (_currentQuestionView, _nextQuestionView) = (_nextQuestionView, _currentQuestionView);
        }
    }
}