using System.Collections.Generic;

namespace BehaviorTree
{
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

        // Realizes shared data so all objects in the behavior tree can access the same objects
        public SharedData sharedData;

        // Child nodes of this node
        protected List<Node> children = new List<Node>();

        public Node()
        {
        }

        public Node(Node child)
        {
            _Attach(child);
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                _Attach(child);
            }
        }

        /// <summary>
        /// Helper-Function to create the two-way connection between this node and new child.
        /// </summary>
        /// <param name="node">Node to add as child</param>
        private void _Attach(Node node)
        {
            children.Add(node);
        }

        /// <summary>
        /// Sets the shared data reference of this node and all child nodes.
        /// Sets specific data fields to proper values for the start of the game.
        /// </summary>
        /// <param name="sharedData">The shared data reference</param>
        public void SetupSharedData(SharedData sharedData)
        {
            foreach (Node node in children)
            {
                node.SetupSharedData(sharedData);
            }

            this.sharedData = sharedData;

            // SetData(sharedData.TargetReached, false);
            // SetData(sharedData.IsAiming, false);
            // SetData(sharedData.IsStunned, false);
            // SetData(sharedData.IsAwareOfPlayer, false);
        }

        /// <summary>
        /// Evaluate-Prototype for other Node-Derivatives to override.
        /// Evaluates the NodeState of this node. 
        /// </summary>
        /// <returns>FAILURE as default</returns>
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData<T>(SharedDataKey<T> key, object value)
        {
            sharedData.SetData(key, (T) value);
        }

        public T GetData<T>(SharedDataKey<T> key)
        {
            return sharedData.GetData(key);
        }

        public bool HasData<T>(SharedDataKey<T> key)
        {
            return sharedData.HasData(key);
        }

        public bool ClearData<T>(SharedDataKey<T> key)
        {
            return sharedData.ClearData(key);
        }
    }
}