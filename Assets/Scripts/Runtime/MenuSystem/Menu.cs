using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame.Runtime.MenuSystem
{
    [RequireComponent(typeof(Canvas))]
    public class Menu : MonoBehaviour
    {
        [SerializeField] private AnimatorStatePlayer entryAnimState;
        [SerializeField] private AnimatorStatePlayer outroAnimState;
        
        private GraphicRaycaster _graphicRaycaster;

        private void Awake()
        {
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            _graphicRaycaster.enabled = false;
        }

        public IEnumerator EntryAnimationRoutine()
        {
            yield return new WaitForSeconds(entryAnimState.Play());
            if (_graphicRaycaster)
            {
                _graphicRaycaster.enabled = true;
            }
        }
        
        public IEnumerator OutroAnimationRoutine()
        {
            if (_graphicRaycaster)
            {
                _graphicRaycaster.enabled = false;
            }
            yield return new WaitForSeconds(outroAnimState.Play());
        }
    }
}