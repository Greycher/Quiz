using System.Collections.Generic;
using QuizGame.Runtime;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace QuizGame.Editor.Editor
{
    [CustomPropertyDrawer(typeof(AnimatorStatePlayer))]
    public class AnimatorStatePlayerDrawer : PropertyDrawer
    {
        private bool _foldout;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var pos = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            _foldout = EditorGUI.Foldout(pos, _foldout, new GUIContent(property.displayName));
            pos.y += EditorGUIUtility.singleLineHeight;

            if (!_foldout)
            {
                return;
            }
            
            EditorGUI.indentLevel++;

            var animatorProp = property.FindPropertyRelative(AnimatorStatePlayer.AnimatorPropertyName);
            EditorGUI.ObjectField(pos, animatorProp, new GUIContent(animatorProp.displayName));
            pos.y += EditorGUIUtility.singleLineHeight;
            
            var layerIndexProp = property.FindPropertyRelative(AnimatorStatePlayer.LayerIndexPropertyName);
            EditorGUI.PropertyField(pos, layerIndexProp, new GUIContent(layerIndexProp.displayName));
            pos.y += EditorGUIUtility.singleLineHeight;
            
            if (!TryDrawAnimatorStateProperty(pos, property, label, out string errorMessage)) 
            {
                EditorGUI.HelpBox(pos, errorMessage, MessageType.Error);
            }
            
            EditorGUI.indentLevel--;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_foldout)
            {
                return 4 * EditorGUIUtility.singleLineHeight;
            }
            
            return EditorGUIUtility.singleLineHeight;
        }

        private bool TryDrawAnimatorStateProperty(Rect position, SerializedProperty property, GUIContent label, out string errorMessage)
        {
            var animator = property.FindPropertyRelative(AnimatorStatePlayer.AnimatorPropertyName).objectReferenceValue as Animator;
            if (!animator) 
            {
                errorMessage = $"{nameof(Animator)} property can not be null. ";
                return false;
            }

            var animatorController = FetchRuntimeAnimatorController(animator);
            if (!animatorController) 
            {
                errorMessage = $"{nameof(Animator)}'s {nameof(AnimatorController)} field is empty!.";
                return false;
            }

            var layerIndex = property.FindPropertyRelative(AnimatorStatePlayer.LayerIndexPropertyName).intValue;
            
            var stateNames = new List<string>(new []{"None"});
            var childStates = animatorController.layers[layerIndex].stateMachine.states;
            for (int i = 0; i < childStates.Length; i++) 
            {
                stateNames.Add(childStates[i].state.name);
            }

            var animatorStateProp = property.FindPropertyRelative(AnimatorStatePlayer.AnimatorStatePropertyName);
            
            var selectedIndex = FindSelectedIndex(stateNames, animatorStateProp.intValue);
            if (selectedIndex == -1) 
            {
                selectedIndex = 0; //"None"
            }
            
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, stateNames.ToArray());
            animatorStateProp.intValue = Animator.StringToHash(stateNames[selectedIndex]);
            
            errorMessage = "";
            return true;
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