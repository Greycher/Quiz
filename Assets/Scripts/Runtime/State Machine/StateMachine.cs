using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{
    public class StateMachine : MonoBehaviour
    {
        private State _currentState;

        private void Update()
        {
            UpdateState();
        }
        
        private void UpdateState()
        {
            _currentState.UpdateState();
        }

        public void TransitionToState(State newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }
    }
}
