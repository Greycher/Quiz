using System;
using ColorTilesHop.Runtime;
using ColorTilesHop.Runtime.Signals;
using Cysharp.Threading.Tasks;
using QuizGame.Runtime.Model;
using QuizGame.Runtime.SettingRegistry.Settings;
using QuizGame.Runtime.View;
using UnityEngine;
using UnityEngine.Assertions;

namespace QuizGame.Runtime.Presenter
{
    public class QuizPresenter : MonoBehaviour
    {
        [SerializeField] private Quiz quiz;
        [SerializeField] private QuizView quizView;
        [SerializeField] private TimerView timerView;
        [SerializeField] private ScoreView scoreView;
        
        private QuizSetting _quizSetting;
        private int _currentQuestionIndex;
        private bool _inQuizSession;
        private int _score;

        private Question CurrentQuestion => quiz.Questions[_currentQuestionIndex];
        
        private void Awake()
        {
            _quizSetting = SettingRegistry.SettingRegistry.Load<QuizSetting>();
            
            scoreView.SetScore(0);
            timerView.gameObject.SetActive(false);
            scoreView.gameObject.SetActive(false);
        }
        
        public void StartQuizSession()
        {
            Assert.IsFalse(_inQuizSession);
            _inQuizSession = true;
            _currentQuestionIndex = 0;
            
            scoreView.gameObject.SetActive(true);
            timerView.gameObject.SetActive(true);
            
            StartQuestionSession();
        }

        private void StartQuestionSession()
        {
            SetQuestionAsync(CurrentQuestion);
        }
        
        private void EnQuestionSession(QuestionResult result)
        {
            quizView.OnAnswerSelect -= OnAnswerSelected;
            timerView.StopTimer();
            _score += _quizSetting.GetQuestionResultScore(result);
        }

        private void EndQuizSession()
        {
            _inQuizSession = false;
            SignalBus<QuizSessionEndSignal>.Emit(new QuizSessionEndSignal(_score));
        }

        private async UniTask SetQuestionAsync(Question question)
        {
            quizView.OnAnswerSelect += OnAnswerSelected;
            await quizView.SetNextQuestionAsync(question);
            timerView.StartTimer(_quizSetting.TimePerQuestion, OnTimerExpired);
        }
        
        private void OnAnswerSelected(Answer answer)
        {
            if (answer == CurrentQuestion.Answer)
            {
                OnCorrectAnswerAsync(answer);
            }
            else
            {
                OnWrongAnswerAsync(answer);
            }
        }

        private async UniTask OnCorrectAnswerAsync(Answer answer)
        {
            EnQuestionSession(QuestionResult.CorrectAnswer);
            await quizView.AnimateSelectedAnswerAsync(answer);
            quizView.VisualiseCorrectAnswer(answer);
            scoreView.SetScoreAnimated(_score);
            ContinueOrEndQuizSessionWithDelayAsync(true);
        }

        private async UniTask OnWrongAnswerAsync(Answer answer)
        {
            EnQuestionSession(QuestionResult.WrongAnswer);
            await quizView.AnimateSelectedAnswerAsync(answer);
            quizView.VisualiseWrongAnswer(answer);
            quizView.VisualiseCorrectAnswer(CurrentQuestion.Answer);
            scoreView.SetScoreAnimated(_score);
            var shouldContinue = _quizSetting.ShouldContinueToNextQuestionOnWrongAnswer;
            ContinueOrEndQuizSessionWithDelayAsync(shouldContinue);
        }

        private void OnTimerExpired()
        {
            EnQuestionSession(QuestionResult.TimeOut);
            quizView.VisualiseCorrectAnswer(CurrentQuestion.Answer);
            scoreView.SetScoreAnimated(_score);
            var shouldContinue = _quizSetting.ShouldContinueToNextQuestionOnTimerExpired;
            ContinueOrEndQuizSessionWithDelayAsync(shouldContinue);
        }
        
        private async UniTask ContinueOrEndQuizSessionWithDelayAsync(bool shouldContinue)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_quizSetting.DelayAfterQuestionSessionEnd));
            if (shouldContinue && ++_currentQuestionIndex < quiz.Questions.Length)
            {
                StartQuestionSession();
            }
            else
            {
                EndQuizSession();
            }
        }
    }
}