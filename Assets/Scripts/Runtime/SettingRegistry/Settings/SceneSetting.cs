using UnityEngine;

namespace QuizGame.Runtime.SettingRegistry.Settings
{
    [CreateAssetMenu(fileName = FileName, menuName =  MenuName + "/" + FileName)]
    public class SceneSetting : Setting
    {
        private const string FileName = nameof(SceneSetting);

        public int MainMenuSceneBuildIndex;
        public int GameSceneBuildIndex;
    }
}