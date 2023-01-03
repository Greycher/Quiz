using System.Collections.Generic;
using QuizGame.Runtime;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace QuizGame.Editor.Editor
{
    //TODO remove repeating code
    [CustomPropertyDrawer(typeof(AnimatorStateAttribute))]
    public class AnimatorStateDrawer : PropertyDrawer
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
                errorMessage = $"{nameof(AnimatorStateDrawer)} works only with integer properties!";
                return false;
            }
            
            var animator = FetchAnimator(property);
            if (!animator) 
            {
                errorMessage = $"{nameof(Animator)} object with the given property name can not be found! " +
                               $"Given parameter name might be wrong or simple reference could be null.";
                return false;
            }

            var animatorController = FetchRuntimeAnimatorController(animator);
            if (!animatorController) 
            {
                errorMessage = $"{nameof(Animator)}'s {nameof(AnimatorController)} field is empty!.";
                return false;
            }
            
            var attr = attribute as AnimatorStateAttribute;
            var stateNames = new List<string>(new []{"None"});
            var childStates = animatorController.layers[attr.Layer].stateMachine.states;
            for (int i = 0; i < childStates.Length; i++) 
            {
                stateNames.Add(childStates[i].state.name);
            }

            var selectedIndex = FindSelectedIndex(stateNames, property.intValue);
            if (selectedIndex == -1) 
            {
                selectedIndex = 0; //"None"
            }
            
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, stateNames.ToArray());
            property.intValue = Animator.StringToHash(stateNames[selectedIndex]);
            
            errorMessage = "";
            return true;
        }

        private Animator FetchAnimator(SerializedProperty property) 
        {
            var attr = attribute as AnimatorStateAttribute;
            var animatorProperty = property.serializedObject.FindProperty(attr.AnimatorPropertyName);
            return animatorProperty.objectReferenceValue as Animator;
        }

        private AnimatorController FetchRuntimeAnimatorController(Animator animator)
        {
            if (animator.runtimeAnimatorController is AnimatorController animatorController)
            {
                return animatorController;
            }

            if (animator.runtimeAnimatorController is AnimatorOverrideController animatorOverrideController)
            {
                return animatorOverrideController.runtimeAnimatorController as AnimatorController;
            }

            return null;
        }
        
        private int FindSelectedIndex(List<string> stateNames, int hashValue) 
        {
            for (int i = 0; i < stateNames.Count; i++) 
            {
                if (Animator.StringToHash(stateNames[i]) == hashValue) 
                {
                    return i;
                }
            }

            return -1;
        }
    }
}