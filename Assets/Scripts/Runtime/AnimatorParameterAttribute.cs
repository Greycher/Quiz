using UnityEngine;

namespace EasyClap.Seneca.Attributes
{
    public class AnimatorParameterAttribute : PropertyAttribute 
    {
        public AnimatorControllerParameterType ParameterType; 
        public string AnimatorPropertyName; 
            
        public AnimatorParameterAttribute(AnimatorControllerParameterType parameterType, string animatorPropertyName) 
        {
            ParameterType = parameterType;
            AnimatorPropertyName = animatorPropertyName;
        }
    }
}