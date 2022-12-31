using UnityEngine;

namespace Quiz
{
    public abstract class State : MonoBehaviour
    {
        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
    }
}