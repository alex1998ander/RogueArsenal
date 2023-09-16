using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class SharedDataKey<T>
    {
        public int Key { get; private set; }

        private static int _keyCounter = 0;

        public SharedDataKey()
        {
            Key = _keyCounter++;
        }
    }

    public class SharedData
    {
        public SharedDataKey<Vector3> PlayerLocation = new();
        public SharedDataKey<Vector3> Target = new();
        public SharedDataKey<bool> TargetReached = new();
        public SharedDataKey<bool> IsAiming = new();
        public SharedDataKey<bool> IsStunned = new();
        public SharedDataKey<bool> IsAwareOfPlayer = new();

        private Dictionary<int, object> _data = new();

        /// <summary>
        /// Sets a key-value-pair of the shared data.
        /// </summary>
        /// <param name="key">Key of new value.</param>
        /// <param name="value">New value.</param>
        public void SetData<T>(SharedDataKey<T> key, T value)
        {
            _data[key.Key] = value;
        }

        /// <summary>
        /// Searches if key has been defined in the behaviour tree.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <returns>Data if key-value-pair was found, null if not.</returns>
        public T GetData<T>(SharedDataKey<T> key)
        {
            object value = null;
            if (_data.TryGetValue(key.Key, out value))
            {
                return (T) value;
            }

            return default(T);
        }

        /// <summary>
        /// Searches for a key inside the tree.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <returns>true if data context contains key, else false.</returns>
        public bool HasData<T>(SharedDataKey<T> key)
        {
            return _data.ContainsKey(key.Key);
        }

        /// <summary>
        /// Removes the key-value-pair from the first node where the key has been found.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <returns>true if data successfully deleted, false if not.</returns>
        public bool ClearData<T>(SharedDataKey<T> key)
        {
            return _data.Remove(key.Key);
        }
    }
}