using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Represents the behaviour tree of an AI.
    /// </summary>
    public abstract class BehaviorTree : MonoBehaviour
    {
        // The root of the behavior tree
        private Node _root;

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
        /// Is called when the enemy is stunned. Sets the appropriate data value in shared data.
        /// </summary>
        public void Stun()
        {
            _root.SetData(_root.sharedData.IsStunned, true);
        }

        /// <summary>
        /// Is called when the enemy hears the player shooting. Sets the appropriate data value in shared data.
        /// </summary>
        public void HearShotFired()
        {
            _root.SetData(_root.sharedData.IsAwareOfPlayer, true);
        }
    }
}