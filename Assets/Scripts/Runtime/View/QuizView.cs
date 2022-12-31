using QuizGame.Runtime.Model;
using TMPro;
using UnityEngine;

namespace QuizGame.Runtime.View
{
    public class QuizView : MonoBehaviour
    {
        [SerializeField] private QuestionView questionView;

        public void SetQuestion(MultipleChoiceQuestion question)
        {
            questionView.SetQuestion(question);
        }
    }
}