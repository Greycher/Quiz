using System;
using UnityEngine;

namespace QuizGame.Runtime.Model
{
    [Serializable]
    public class Question
    {
        [SerializeField] private string category;
        [SerializeField] private string questionText;
        [SerializeField] private string[] choices;
        [SerializeField] private Answer answer;

        public Question(string category, string question, string[] choices, string answer)
        {
            this.category = category.TrimAndRemoveNewLines();
            questionText = question.TrimAndRemoveNewLines();
            this.choices = new string[choices.Length];
            for (int i = 0; i < choices.Length; i++)
            {
                this.choices[i] = choices[i].TrimAndRemoveNewLines();
            }
            this.answer = ParseStringToAnswer(answer);
        }

        public string Category => category;
        public string QuestionText => questionText;
        public string ChoiceA => choices[0];
        public string ChoiceB => choices[1];
        public string ChoiceC => choices[2];
        public string ChoiceD => choices[3];
        public Answer Answer => answer;

        private Answer ParseStringToAnswer(string answerStr)
        {
            switch (answerStr.TrimAndRemoveNewLines())
            {
                case "A":
                    return Answer.A;
                case "B":
                    return Answer.B;
                case "C":
                    return Answer.C;
                case "D":
                    return Answer.D;
                default:
                    throw new Exception("Given unexpected answer string!");
            }
        }
    }
}