using System;
using System.Collections;
using Newtonsoft.Json;
using QuizGame.Runtime.Model;
using QuizGame.Runtime.View;
using UnityEngine;
using UnityEngine.Networking;

namespace QuizGame.Runtime.Presenter
{
    public class QuizPresenter : MonoBehaviour
    {
        [SerializeField] private QuizView quizView;
        
        private Quiz _quiz;
        
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
                Debug.Log("Quiz created succesfully.");
                quizView.SetQuestion(_quiz.Questions[0]);
            }
            catch (JsonReaderException  e)
            {
                Debug.Log($"Invalid character at position {e.LineNumber}, {e.LinePosition}");
                throw;
            }
        }
        
        
    }
}