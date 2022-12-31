using System;
using UnityEngine;

namespace QuizGame.Runtime.Model
{
    [Serializable]
    public class Quiz
    {
        [SerializeField] private MultipleChoiceQuestion[] questions;

        public MultipleChoiceQuestion[] Questions => questions;

        public Quiz(MultipleChoiceQuestion[] questions)
        {
            this.questions = questions;
        }

        public void Log()
        {
            foreach (var question in questions)
            {
                question.Log();
            }
        }
    }
}