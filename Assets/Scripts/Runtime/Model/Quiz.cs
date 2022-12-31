using System;

namespace Quiz.Runtime.Model
{
    [Serializable]
    public class Quiz
    {
        public Question[] questions;

        public void Log()
        {
            foreach (var question in questions)
            {
                question.Log();
            }
        }
    }
}