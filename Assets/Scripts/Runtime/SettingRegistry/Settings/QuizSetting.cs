using System;
using QuizGame.Runtime.Model;
using UnityEngine;

namespace QuizGame.Runtime.SettingRegistry.Settings
{
    [CreateAssetMenu(fileName = FileName, menuName =  MenuName + "/" + FileName)]
    public class QuizSetting : Setting
    {
        private const string FileName = nameof(QuizSetting);

        [SerializeField] private int correctAnswerScore = 5;
        [SerializeField] private int wrongAnswerScore = -5;
        [SerializeField] private int timeOutScore = -3;

        public int GetQuestionResultScore(QuestionResult questionResult)
        {
            switch (questionResult)
            {
                case QuestionResult.CorrectAnswer: 
                    return correctAnswerScore;
                case QuestionResult.WrongAnswer: 
                    return wrongAnswerScore;
                case QuestionResult.TimeOut: 
                    return timeOutScore;
            }

            throw new Exception();
        }
    }
}