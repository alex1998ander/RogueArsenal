using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Represents the behaviour tree of an AI.
    /// </summary>
    public abstract class BehaviorTree : MonoBehaviour
    {
        // The root of the behavior tree
        protected Node root;

        /// <summary>
        /// On game start, builds the behaviour tree.
        /// </summary>
        protected void Awake()
        {
            root = SetupTree();
        }

        /// <summary>
        /// Every fixed update, evaluate the tree.
        /// </summary>
        protected void FixedUpdate()
        {
            if (root != null)
            {
                root.Evaluate();
            }
        }

        /// <summary>
        /// Creates the behaviour tree.
        /// Each derivative of Tree needs to override this method to create a behaviour tree.
        /// </summary>
        /// <returns>The root node of the new behaviour tree.</returns>
        protected abstract Node SetupTree();
    }
}