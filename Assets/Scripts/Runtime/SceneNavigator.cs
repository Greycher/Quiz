using QuizGame.Runtime.SettingRegistry.Settings;
using UnityEngine.SceneManagement;

namespace QuizGame.Runtime
{
    public static class SceneNavigator
    {
        private static SceneSetting Setting => SettingRegistry.SettingRegistry.Load<SceneSetting>();
        
        public static void LoadMainMenuScene()
        {
            SceneManager.LoadScene(Setting.MainMenuSceneBuildIndex);
        }
        
        public static void LoadGameScene()
        {
            SceneManager.LoadScene(Setting.GameSceneBuildIndex);
        }
    }
}