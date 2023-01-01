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
        
        private Quiz _quiz;
        private int _currentQuestionIndex;
        
        private Question CurrentQuestion => _quiz.Questions[_currentQuestionIndex];
        
        private void Awake()
        {
            StartCoroutine(FetchQuizRoutine());
        }

        private void OnEnable()
        {
            quizView.OnAsnwerSelect += OnAsnwerSelected;
        }
        
        private void OnDisable()
        {
            quizView.OnAsnwerSelect -= OnAsnwerSelected;
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
                Debug.Log($"Invalid character at position {e.LineNumber}, {e.LinePosition}");
                throw;
            }
        }

        private void OnAsnwerSelected(Answer answer)
        {
            Debug.Log($"The given answer is {(answer == CurrentQuestion.Answer).ToString()}");
        }
        
        [Button]
        private void SetNextQuestion()
        {
            StartCoroutine(quizView.SetNextQuestionRoutine(CurrentQuestion));
            _currentQuestionIndex++;
        }
        
        // public void StartQuiz()
        // {
        //     StartCoroutine(StartQuiRoutine());
        // }
        //
        // private IEnumerator StartQuiRoutine()
        // {
        //     _currentQuestion = _quiz.Questions[0];
        //     yield return quizView.SetNextQuestionRoutine(_currentQuestion);
        // }
    }
}