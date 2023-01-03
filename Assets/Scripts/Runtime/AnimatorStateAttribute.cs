using UnityEngine;

namespace QuizGame.Runtime
{
    public class AnimatorStateAttribute : PropertyAttribute
    {
        public string AnimatorPropertyName;
        public int Layer;

        public AnimatorStateAttribute(string animatorPropertyName, int layer = 0)
        {
            AnimatorPropertyName = animatorPropertyName;
            Layer = layer;
        }
    }
}