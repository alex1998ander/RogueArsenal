using System.Collections.Generic;

namespace BehaviorTree
{
    public static class SharedData
    {
        public static string PlayerLocation = "playerLocation";
        public static string Target = "target";
        public static string TargetReached = "targetReached";
        public static string IsAiming = "isAiming";
        public static string IsStunned = "isStunned";
        public static string IsAwareOfPlayer = "isAwareOfPlayer";
    }

    /// <summary>
    /// Execution state of a node.
    /// </summary>
    public enum NodeState
    {
        RUNNING, // Node is currently evaluating.
        SUCCESS, // Node evaluation has succeeded.
        FAILURE // Node evaluation has failed.
    }

    /// <summary>
    /// Represents a node of the behaviour tree.
    /// A "leaf" (Node without children) contains the tasks that are performed by the AI.
    /// An "internal node" (All non-leaf nodes) defines the logic structure and
    /// switches from one behavior branch to another.
    /// </summary>
    public class Node
    {
        // The state of this node
        protected NodeState state;

        // Parent node of this node
        public Node parent;

        // Child nodes of this node
        protected List<Node> children = new List<Node>();

        // Realizes shared data so all objects in the behavior tree can access the same objects
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
            SetupData();
        }

        public Node(Node child)
        {
            _Attach(child);
            SetupData();
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                _Attach(child);
            }

            SetupData();
        }

        /// <summary>
        /// Helper-Function to create the two-way connection between this node and new child.
        /// </summary>
        /// <param name="node">Node to add as child</param>
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        /// <summary>
        /// Evaluate-Prototype for other Node-Derivatives to override.
        /// Evaluates the NodeState of this node. 
        /// </summary>
        /// <returns>FAILURE as default</returns>
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        /// <summary>
        /// Sets a key-value-pair in the root node of the tree.
        /// </summary>
        /// <param name="key">Key of new value.</param>
        /// <param name="value">New value.</param>
        public void SetDataInRoot(string key, object value)
        {
            Node node = this;
            while (node.parent != null)
            {
                node = node.parent;
            }

            node.SetData(key, value);
        }

        /// <summary>
        /// Sets a key-value-pair of the shared data.
        /// </summary>
        /// <param name="key">Key of new value.</param>
        /// <param name="value">New value.</param>
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        /// <summary>
        /// Searches if key has been defined in the behaviour tree.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <returns>Data if key-value-pair was found, null if not.</returns>
        public T GetData<T>(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
            {
                return (T) value;
            }

            Node node = parent;
            while (node != null)
            {
                value = node.GetData<T>(key);
                if (value != null)
                {
                    return (T) value;
                }

                node = node.parent;
            }

            return default;
        }

        /// <summary>
        /// Searches for a key inside the tree.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <returns>true if data context contains key, else false.</returns>
        public bool HasData(string key)
        {
            Node node = this;
            while (node != null)
            {
                if (_dataContext.ContainsKey(key))
                    return true;

                node = node.parent;
            }

            return false;
        }

        /// <summary>
        /// Removes the key-value-pair from the first node where the key has been found.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <returns>true if data successfully deleted, false if not.</returns>
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }

                node = node.parent;
            }

            return false;
        }

        private void SetupData()
        {
            SetDataInRoot(SharedData.TargetReached, false);
            SetDataInRoot(SharedData.IsAiming, false);
            SetDataInRoot(SharedData.IsStunned, false);
        }
    }
}