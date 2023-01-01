using System;
using UnityEngine;

namespace QuizGame.Runtime.Model
{
    [Serializable]
    public class Quiz
    {
        [SerializeField] private Question[] questions;

        public Question[] Questions => questions;

        public Quiz(Question[] questions)
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