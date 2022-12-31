using System;
using UnityEngine;

namespace Quiz.Runtime.Model
{
    [Serializable]
    public class Question
    {
        public string category;
        public string question;
        public string[] choices;
        public string answer;

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