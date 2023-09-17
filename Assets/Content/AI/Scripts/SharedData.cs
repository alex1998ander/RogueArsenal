using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Class representing the key for a data object of a specific type. 
    /// </summary>
    /// <typeparam name="T">The type of this data object</typeparam>
    public class SharedDataType<T>
    {
        public int Key { get; }

        private static int _keyCounter = 0;

        public SharedDataType()
        {
            Key = _keyCounter++;
        }
    }

    public class SharedData
    {
        // Shared data types of all possibly relevant data used in the behavior trees
        public SharedDataType<Vector3> LastKnownPlayerLocation = new();
        public SharedDataType<Vector3> Target = new();
        public SharedDataType<bool> TargetReached = new();
        public SharedDataType<bool> IsAiming = new();
        public SharedDataType<bool> IsStunned = new();
        public SharedDataType<bool> IsAwareOfPlayer = new();

        // Data container
        private Dictionary<int, object> _data = new();

        /// <summary>
        /// Sets a key-value-pair of the shared data.
        /// </summary>
        /// <param name="type">Key of new value.</param>
        /// <param name="value">New value.</param>
        public void SetData<T>(SharedDataType<T> type, T value)
        {
            _data[type.Key] = value;
        }

        /// <summary>
        /// Searches if key has been defined in the behaviour tree.
        /// </summary>
        /// <param name="type">Key to search for.</param>
        /// <returns>Data if key-value-pair was found, null if not.</returns>
        public T GetData<T>(SharedDataType<T> type)
        {
            object value = null;
            if (_data.TryGetValue(type.Key, out value))
            {
                return (T) value;
            }

            return default;
        }

        /// <summary>
        /// Searches for a key inside the tree.
        /// </summary>
        /// <param name="type">Key to search for.</param>
        /// <returns>true if data context contains key, else false.</returns>
        public bool HasData<T>(SharedDataType<T> type)
        {
            return _data.ContainsKey(type.Key);
        }

        /// <summary>
        /// Removes the key-value-pair from the first node where the key has been found.
        /// </summary>
        /// <param name="type">Key to search for.</param>
        /// <returns>true if data successfully deleted, false if not.</returns>
        public bool ClearData<T>(SharedDataType<T> type)
        {
            return _data.Remove(type.Key);
        }
    }
}