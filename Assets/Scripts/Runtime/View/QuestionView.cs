using QuizGame.Runtime.Model;
using TMPro;
using UnityEngine;

namespace QuizGame.Runtime.View
{
    public class QuestionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI questionLabel;
        [SerializeField] private TextMeshProUGUI choiceALabel;
        [SerializeField] private TextMeshProUGUI choiceBLabel;
        [SerializeField] private TextMeshProUGUI choiceCLabel;
        [SerializeField] private TextMeshProUGUI choiceDLabel;

        public void SetQuestion(MultipleChoiceQuestion multipleChoiceQuestion)
        {
            questionLabel.text = multipleChoiceQuestion.Question;
            choiceALabel.text = multipleChoiceQuestion.ChoiceA;
            choiceBLabel.text = multipleChoiceQuestion.ChoiceB;
            choiceCLabel.text = multipleChoiceQuestion.ChoiceC;
            choiceDLabel.text = multipleChoiceQuestion.ChoiceD;
        }
    }
}