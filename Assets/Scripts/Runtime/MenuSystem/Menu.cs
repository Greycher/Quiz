using System;
using System.Collections;
using Cysharp.Threading.Tasks;
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

        private bool Interactable
        {
            set
            {
                if (_graphicRaycaster)
                {
                    _graphicRaycaster.enabled = value;
                }
            }
        }

        private void Awake()
        {
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            Interactable = false;
        }

        public async UniTask EntryAnimationRoutine()
        {
            var d = entryAnimState.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(d));
            Interactable = true;
        }
        
        public async UniTask OutroAnimationRoutine()
        {
            Interactable = false;
            var d = outroAnimState.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(d));
        }
    }
}