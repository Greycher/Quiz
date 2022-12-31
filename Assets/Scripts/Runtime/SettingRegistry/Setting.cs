using UnityEngine;

namespace QuizGame.Runtime.SettingRegistry
{
    public abstract class Setting : ScriptableObject
    {
        protected const string MenuName = nameof(Setting) + "s";
    }
}