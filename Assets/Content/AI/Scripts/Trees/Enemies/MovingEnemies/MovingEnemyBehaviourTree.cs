using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class MovingEnemyBehaviourTree : EnemyBehaviourTree
    {
        [SerializeField] protected float movementSpeed = 20f;
        [SerializeField] protected float movementAcceleration = 30f;
        [SerializeField] protected float minDistanceFromPlayer = 4f;
        [SerializeField] protected float maxDistanceFromPlayer = 6f;

        protected override Node SetupTree()
        {
            base.SetupTree();
            
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            Node root = new Node();
            return root;
        }
    }
}