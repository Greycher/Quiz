using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame.Runtime.SettingRegistry.Settings
{
    [CreateAssetMenu(fileName = FileName, menuName =  MenuName + "/" + FileName)]
    public class AdvancedUIMenuItemSetting : Setting
    {
        private const string FileName = nameof(AdvancedUIMenuItemSetting);

        public Canvas BaseRootCanvas;
        public TextMeshProUGUI BaseLabelPrefab;
        public Image BaseImagePrefab;
        public Button BaseButtonPrefab;
        public Button BaseLabeledButtonPrefab;
    }
}