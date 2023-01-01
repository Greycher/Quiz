using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace EasyClap.Seneca.Attributes
{
    [CustomPropertyDrawer(typeof(AnimatorParameterAttribute))]
    public class AnimatorParameterDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            if (!TryDrawCustomGUI(position, property, label, out string errorMessage)) 
            {
                EditorGUI.HelpBox(position, errorMessage, MessageType.Error);
            }
        }

        private bool TryDrawCustomGUI(Rect position, SerializedProperty property, GUIContent label, out string errorMessage) 
        {
            if (property.propertyType != SerializedPropertyType.Integer) 
            {
                errorMessage = $"{nameof(AnimatorParameterDrawer)} works only with integer properties!";
                return false;
            }
            
            var animator = FetchAnimator(property);
            if (!animator) 
            {
                errorMessage = $"{nameof(Animator)} object with the given property name can not be found! " +
                               $"Given parameter name might be wrong or simple reference could be null.";
                return false;
            }

            var animatorController = animator.runtimeAnimatorController as AnimatorController;
            if (!animatorController) 
            {
                errorMessage = $"{nameof(Animator)}'s {nameof(AnimatorController)} field is empty!.";
                return false;
            }
            
            var attr = attribute as AnimatorParameterAttribute;
            var parameterNames = new List<string>(new []{"None"});
            var parameters = animatorController.parameters;
            for (int i = 0; i < parameters.Length; i++) 
            {
                var parameter = parameters[i];
                if (parameter.type == attr.ParameterType) 
                {
                    parameterNames.Add(parameter.name);
                }
            }

            var selectedIndex = FindSelectedIndex(parameterNames, property.intValue);
            if (selectedIndex == -1) 
            {
                selectedIndex = 0; //"None"
            }
            
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, parameterNames.ToArray());
            if (selectedIndex != 0) 
            {
                property.intValue = Animator.StringToHash(parameterNames[selectedIndex]);
            }
            
            errorMessage = "";
            return true;
        }

        private Animator FetchAnimator(SerializedProperty property) 
        {
            var attr = attribute as AnimatorParameterAttribute;
            var animatorProperty = property.serializedObject.FindProperty(attr.AnimatorPropertyName);
            return animatorProperty.objectReferenceValue as Animator;
        }
        
        private int FindSelectedIndex(List<string> parameterNames, int hashValue) 
        {
            for (int i = 0; i < parameterNames.Count; i++) 
            {
                if (Animator.StringToHash(parameterNames[i]) == hashValue) 
                {
                    return i;
                }
            }

            return 0;
        }
    }
}