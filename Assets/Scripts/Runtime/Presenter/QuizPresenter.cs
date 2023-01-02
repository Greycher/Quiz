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
        }

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
        
        private void AskQuestion()
        {
            StartCoroutine(AskQuestionRoutine(CurrentQuestion));
        }

        private IEnumerator AskQuestionRoutine(Question question)
        {
            yield return quizView.SetNextQuestionRoutine(question);
            timerView.StartTimer(20, OnTimerCompleted);
        }
        
        private void OnAnswerSelected(Answer answer)
        {
            timerView.StopTimer();
            if (answer == CurrentQuestion.Answer)
            {
                if (++_currentQuestionIndex < _quiz.Questions.Length)
                {
                    AskQuestion();
                }
                else
                {
                    EndQuiz(true);
                }
            }
            else
            {
                EndQuiz(false);
            }
        }

        private void OnTimerCompleted()
        {
            EndQuiz(false);
        }
        
        //TODO Make sure quiz model creation was successfull
        [Button]
        public void StartQuiz()
        {
            _currentQuestionIndex = 0;
            quizView.OnAnswerSelect += OnAnswerSelected;

            AskQuestion();
        }

        private void EndQuiz(bool success)
        {
            quizView.OnAnswerSelect -= OnAnswerSelected;
            Debug.Log($"The given answer is {success.ToString()}");
        }
    }
}