using System;
using UnityEngine;

namespace QuizGame.Runtime.Model
{
    [Serializable]
    public class Quiz
    {
        [SerializeField] private Question[] questions;

        public Question[] Questions => questions;
        public int Score { get; set; }

        public Quiz(Question[] questions)
        {
            this.questions = questions;
        }
    }
}