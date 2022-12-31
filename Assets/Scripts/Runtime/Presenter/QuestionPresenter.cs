using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;


namespace Quiz.Runtime.Presenter
{
    public class QuestionPresenter : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(GetText());
        }
        
        IEnumerator GetText()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://magegamessite.web.app/case1/questions.json");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                var quiz = JsonConvert.DeserializeObject<Model.Quiz>(request.downloadHandler.text);
                foreach (var question in quiz.questions)
                {
                    Debug.Log(question.category);
                    Debug.Log(question.question);
                    foreach (var choice in question.choices)
                    {
                        Debug.Log(choice);
                    }
                    Debug.Log(question.answer);
                }
            }
        }
    }
}