using Quiz.Runtime.Model;
using TMPro;
using UnityEngine;

namespace Quiz.Runtime.View
{
    public class QuestionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI questionLabel;
        [SerializeField] private TextMeshProUGUI answarALabel;
        [SerializeField] private TextMeshProUGUI answarBLabel;
        [SerializeField] private TextMeshProUGUI answarCLabel;
        [SerializeField] private TextMeshProUGUI answarDLabel;

        public void UpdateQuestion(Question question)
        {
            
        }
    }
}