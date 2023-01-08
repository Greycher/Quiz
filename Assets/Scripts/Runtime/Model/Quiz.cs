using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QuizGame.Runtime.Model
{
    [CreateAssetMenu(fileName = FileName, menuName = FileName)]
    public class Quiz : ScriptableObject
    {
        private const string FileName = nameof(Quiz);

        [SerializeField] private Question[] questions;

        public Question[] Questions => questions;

        public Quiz(Question[] questions)
        {
            this.questions = questions;
        }
        
        [Button]
        public void FromJson(string json)
        {
            try
            {
                questions = JsonConvert.DeserializeObject<Quiz>(json).questions;
            }
            catch (JsonReaderException  e)
            {
                throw new Exception($"Invalid character at position {e.LineNumber}, {e.LinePosition}");
            }
        }
    }
}