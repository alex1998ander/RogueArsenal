using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Represents the behaviour tree of an AI.
    /// </summary>
    public abstract class BTree : MonoBehaviour
    {
        // The root of the behavior tree
        private Node _root = null;

        /// <summary>
        /// On game start, builds the behaviour tree.
        /// </summary>
        protected void Start()
        {
            _root = SetupTree();
        }

        /// <summary>
        /// Every fixed update, evaluate the tree.
        /// </summary>
        protected void FixedUpdate()
        {
            if (_root != null)
            {
                _root.Evaluate();
            }
        }

        /// <summary>
        /// Creates the behaviour tree.
        /// Each derivative of Tree needs to override this method to create a behaviour tree.
        /// </summary>
        /// <returns>The root node of the new behaviour tree.</returns>
        protected abstract Node SetupTree();

        /// <summary>
        /// Makes the enemy stunned
        /// </summary>
        public void Stun()
        {
            _root.SetData(_root.sharedData.IsStunned, true);
        }

        public void HearShotFired()
        {
            _root.SetData(_root.sharedData.IsAwareOfPlayer, true);
            Debug.Log("aware: " + _root.GetData(_root.sharedData.IsAwareOfPlayer));
        }
    }
}