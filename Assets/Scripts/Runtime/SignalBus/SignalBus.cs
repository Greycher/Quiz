using System.Collections.Generic;
using UnityEngine;

namespace ColorTilesHop.Runtime
{
    public static class SignalBus<T> where T : struct
    {
        private static readonly List<OnSignal> _listeners = new List<OnSignal>();
        
        public delegate void OnSignal(T t);

        public static void AddListener(OnSignal onSignal)
        {
            _listeners.Add(onSignal);
        }
        
        public static void RemoveListener(OnSignal onSignal)
        {
            _listeners.Remove(onSignal);
        }

        public static void Emit(T t)
        {
            Debug.Log($"{typeof(T).Name} emitted.");
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i](t);
            }
        }
    }
}