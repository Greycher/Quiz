using System;
using UnityEngine;

namespace QuizGame.Runtime
{
    [Serializable]
    public class AnimatorStatePlayer
    {
        public const string AnimatorPropertyName = nameof(animator);
        public const string LayerIndexPropertyName = nameof(layerIndex);
        public const string AnimatorStatePropertyName = nameof(animatorState);
        
        [SerializeField] private Animator animator;
        [SerializeField] private int layerIndex;
        [SerializeField] private int animatorState;

        public float Play()
        {
            if (!animator || !animator.HasState(layerIndex, animatorState))
            {
                return 0;
            }
            
            animator.Play(animatorState, layerIndex);
            animator.Update(0);
            return animator.GetCurrentAnimatorStateInfo(layerIndex).length;
        }
    }
}