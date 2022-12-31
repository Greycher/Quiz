using System;
using UnityEngine;

namespace QuizGame.Runtime.SettingRegistry
{
    [CreateAssetMenu(fileName = FileName)]
    public class SettingRegistry : ScriptableObject
    {
        private const string FileName = nameof(SettingRegistry);
        
        public Setting[] Settings;

        private static SettingRegistry _instance;

        private static SettingRegistry Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = Resources.Load<SettingRegistry>(FileName);
                    if (!_instance)
                    {
                        throw new Exception($"You must create a {nameof(SettingRegistry)} " +
                                            $"with the name of '{FileName}' under a {nameof(Resources)} folder.");
                    }
                }

                return _instance;
            }
        }
        
        public static T Load<T>() where T : Setting
        {
            foreach (var setting in Instance.Settings)
            {
                if (setting is T t)
                {
                    return t;
                }
            }

            throw new Exception($"Given {nameof(Setting)} type has not registered to {nameof(SettingRegistry)}!");
        }
    }
}