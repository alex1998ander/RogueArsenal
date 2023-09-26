using UnityEngine;

namespace BehaviorTree
{
    public class MovingEnemyBT : EnemyBT
    {
        [SerializeField] protected float movementSpeed = 20f;
        [SerializeField] protected float movementAcceleration = 30f;
        [SerializeField] protected float minDistanceFromPlayer = 4f;
        [SerializeField] protected float maxDistanceFromPlayer = 6f;

        protected override Node SetupTree()
        {
            Node root = new Node();
            return root;
        }
    }
}