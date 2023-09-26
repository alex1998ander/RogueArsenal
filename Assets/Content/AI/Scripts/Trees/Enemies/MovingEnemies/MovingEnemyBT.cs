using UnityEngine;

namespace BehaviorTree
{
    public class MovingEnemyBT : EnemyBT
    {
        // Walking speed.
        [SerializeField] protected float movementSpeed = 20f;

        // Size of the bounding box around the player where the enemy IS NOT allowed to move.
        [SerializeField] protected float minDistanceFromPlayer = 4f;

        // Size of the bounding box around the player where the enemy IS allowed to move.
        [SerializeField] protected float maxDistanceFromPlayer = 6f;

        protected override Node SetupTree()
        {
            Node root = new Node();
            return root;
        }
    }
}