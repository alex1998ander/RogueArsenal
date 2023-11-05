using UnityEngine;

namespace BehaviorTree
{
    public class EnemyBehaviourTree : BehaviorTree, ICharacterController
    {
        // Layer mask of the walls of the level.
        [SerializeField] protected LayerMask wallLayer;

        // TODO: Add these to Configuration library

        // Amount of time the enemy is stunned
        [SerializeField] protected float stunTime;

        // Amount of time the enemy is immune to getting stunned after getting out of a stun
        [SerializeField] protected float stunImmunityTime;

        // Amount of time the enemy is thrown
        [SerializeField] protected float thrownTime;

        // Amount of time the enemy is immune to getting thrown after being thrown
        [SerializeField] protected float thrownImmunityTime;

        // Distance how far the enemy can see
        [SerializeField] protected float viewDistance = 6f;

        // Distance how far the enemy can hear the player shots
        [SerializeField] protected float hearDistance = 20f;

        protected override Node SetupTree()
        {
            EventManager.OnPlayerShotFired.Subscribe(HearPlayerShotFired);

            Node root = new Node();
            return root;
        }

        /// <summary>
        /// Set appropriate data when the enemy is supposed to be stunned.
        /// </summary>
        public void StunCharacter()
        {
            root.SetData(root.sharedData.IsStunned, true);
        }

        /// <summary>
        /// Set appropriate data when the enemy is supposed to be thrown.
        /// Returns whether the enemy could be thrown or not.
        /// TODO? Add to ICharacterController interface
        /// </summary>
        /// <returns>Whether the enemy could be thrown or not.</returns>
        public virtual bool TryThrow()
        {
            root.SetData(root.sharedData.IsThrown, true);
            return true;
        }

        /// <summary>
        /// Set appropriate data when the player fired a shot.
        /// </summary>
        private void HearPlayerShotFired()
        {
            root.SetData(root.sharedData.HasHeardPlayerShot, true);
        }
    }
}