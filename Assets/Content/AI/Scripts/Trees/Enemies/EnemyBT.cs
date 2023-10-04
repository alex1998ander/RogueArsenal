using UnityEngine;

namespace BehaviorTree
{
    public class EnemyBT : BehaviorTree
    {
        // Layer mask of the walls of the level.
        [SerializeField] protected LayerMask wallLayer;

        // Amount of time the enemy is stunned
        [SerializeField] protected float stunTime;

        // Distance how far the enemy can see
        [SerializeField] protected float viewDistance = 6f;

        // Distance how far the enemy can hear the player shots
        [SerializeField] protected float hearDistance = 20f;

        protected override Node SetupTree()
        {
            Node root = new Node();
            return root;
        }
    }
}