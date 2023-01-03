using System;
using System.Collections;
using Newtonsoft.Json;
using QuizGame.Runtime.Model;
using QuizGame.Runtime.View;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;

namespace QuizGame.Runtime.Presenter
{
    public class QuizPresenter : MonoBehaviour
    {
        [SerializeField] private QuizView quizView;
        [SerializeField] private TimerView timerView;
        
        private Quiz _quiz;
        private int _currentQuestionIndex;
        
        private Question CurrentQuestion => _quiz.Questions[_currentQuestionIndex];
        
        private void Awake()
        {
            StartCoroutine(FetchQuizRoutine());
            timerView.gameObject.SetActive(false);
        }

        //TODO background task manager
        IEnumerator FetchQuizRoutine()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://magegamessite.web.app/case1/questions.json");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                throw new Exception(request.error);
            }
            
            try
            {
                _quiz = JsonConvert.DeserializeObject<Quiz>(request.downloadHandler.text);
                Debug.Log("Quiz creation succeed.");
            }
            catch (JsonReaderException  e)
            {
                throw new Exception($"Invalid character at position {e.LineNumber}, {e.LinePosition}");
            }
        }
        
        //TODO Make sure quiz model creation was successfull
        [Button]
        public void StartQuiz()
        {
            _currentQuestionIndex = 0;
            AskQuestion();
        }
        
        private void AskQuestion()
        {
            StartCoroutine(AskQuestionRoutine(CurrentQuestion));
        }
        
        private IEnumerator AskQuestionRoutine(Question question)
        {
            yield return quizView.SetNextQuestionRoutine(question);
            quizView.OnAnswerSelect += OnAnswerSelected;
            timerView.gameObject.SetActive(true);
            //TODO take seconds from a setting
            timerView.StartTimer(20, OnTimerCompleted);
        }
        
        private void OnAnswerSelected(Answer answer)
        {
            timerView.StopTimer();
            quizView.OnAnswerSelect -= OnAnswerSelected;
            if (answer == CurrentQuestion.Answer)
            {
                StartCoroutine(OnCorrectAnswer(answer));
            }
            else
            {
                StartCoroutine(OnWrongAnswer(answer));
            }
        }

        private IEnumerator OnCorrectAnswer(Answer answer)
        {
            yield return quizView.AnimateCorrectAnswer(answer);
            if (++_currentQuestionIndex < _quiz.Questions.Length)
            {
                AskQuestion();
            }
            else
            {
                EndQuiz(true);
            }
        }
        
        private IEnumerator OnWrongAnswer(Answer answer)
        {
            yield return quizView.AnimateWrongAnswer(answer);
            EndQuiz(false);
        }

        private void OnTimerCompleted()
        {
            quizView.OnAnswerSelect -= OnAnswerSelected;
            EndQuiz(false);
        }

        private void EndQuiz(bool success)
        {
            Debug.Log($"Quiz resulted in {(success ? "success" : "fail")}");
        }
    }
}