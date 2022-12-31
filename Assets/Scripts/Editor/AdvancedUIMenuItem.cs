using System;
using System.Reflection;
using QuizGame.Runtime.SettingRegistry;
using QuizGame.Runtime.SettingRegistry.Settings;
using UnityEditor;
using UnityEngine;

namespace QuizGame.Editor.Editor
{
    public class AdvancedUIMenuItem
    {
        private const string _menuItemPath = "GameObject/Base UI/";
        private static MethodInfo _removeMenuItemMethodInfo;
        private static AdvancedUIMenuItemSetting _setting;

        private static AdvancedUIMenuItemSetting Setting
        {
            get
            {
                if (!_setting)
                {
                    _setting = SettingRegistry.Load<AdvancedUIMenuItemSetting>();
                }

                return _setting;
            }
        }
        
        [MenuItem(_menuItemPath + nameof(RootCanvas), false, Int32.MaxValue)]
        static void RootCanvas()
        {
            var element = PrefabUtility.InstantiatePrefab(Setting.BaseRootCanvas, GetTransform()) as Component;
            Undo.RegisterCreatedObjectUndo(element.gameObject, element.name);
            Selection.activeObject = element.gameObject;
        }

        [MenuItem(_menuItemPath + nameof(Label), false, Int32.MaxValue)]
        static void Label()
        {
            var element = PrefabUtility.InstantiatePrefab(Setting.BaseLabelPrefab, GetTransform())  as Component;
            Undo.RegisterCreatedObjectUndo(element.gameObject, element.name);
            Selection.activeObject = element.gameObject;
        }
        
        [MenuItem(_menuItemPath + nameof(Image), false, Int32.MaxValue)]
        static void Image()
        {
            var element = PrefabUtility.InstantiatePrefab(Setting.BaseImagePrefab, GetTransform())  as Component;
            Undo.RegisterCreatedObjectUndo(element.gameObject, element.name);
            Selection.activeObject = element.gameObject;
        }
        
        [MenuItem(_menuItemPath + nameof(Button), false, Int32.MaxValue)]
        static void Button()
        {
            var element = PrefabUtility.InstantiatePrefab(Setting.BaseButtonPrefab, GetTransform())  as Component;
            Undo.RegisterCreatedObjectUndo(element.gameObject, element.name);
            Selection.activeObject = element.gameObject;
        }
        
        [MenuItem(_menuItemPath + nameof(LabeledButton), false, Int32.MaxValue)]
        static void LabeledButton()
        {
            var element = PrefabUtility.InstantiatePrefab(Setting.BaseLabeledButtonPrefab, GetTransform()) as Component;
            Undo.RegisterCreatedObjectUndo(element.gameObject, element.name);
            Selection.activeObject = element.gameObject;
        }

        static Transform GetTransform()
        {
            var parent = Selection.activeTransform;
            if (parent != null && !parent.gameObject.scene.isLoaded)
            {
                parent = null;
            }

            return parent;
        }
    }
}