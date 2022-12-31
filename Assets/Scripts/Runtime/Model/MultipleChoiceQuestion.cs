using System;
using UnityEngine;

namespace QuizGame.Runtime.Model
{
    [Serializable]
    public class MultipleChoiceQuestion
    {
        [SerializeField] private string category;
        [SerializeField] private string question;
        [SerializeField] private string[] choices;
        [SerializeField] private string answer;

        public MultipleChoiceQuestion(string category, string question, string[] choices, string answer)
        {
            this.category = category.TrimAndRemoveNewLines();
            this.question = question.TrimAndRemoveNewLines();
            this.choices = new string[choices.Length];
            for (int i = 0; i < choices.Length; i++)
            {
                this.choices[i] = choices[i].TrimAndRemoveNewLines();
            }
            this.answer = answer.TrimAndRemoveNewLines();
        }

        public string Category => category;
        public string Question => question;
        public string ChoiceA => choices[0];
        public string ChoiceB => choices[1];
        public string ChoiceC => choices[2];
        public string ChoiceD => choices[3];
        public string Answer => answer;

        public void Log()
        {
            Debug.Log(category);
            Debug.Log(question);
            foreach (var choice in choices)
            {
                Debug.Log(choice);
            }
            Debug.Log(answer);
        }
    }
}