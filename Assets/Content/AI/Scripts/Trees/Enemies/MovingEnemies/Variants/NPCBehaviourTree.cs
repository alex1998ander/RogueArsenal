using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class NPCBehaviourTree : MovingEnemyBehaviourTree
    {
        [SerializeField] GameObject exitPoint;

        protected override Node SetupTree()
        {
            base.SetupTree();

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            SharedData sharedData = new SharedData();
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new ExpectData<bool>(sharedData.HasHeardPlayerShot, false),
                    new TaskLookAtMovementDirection(rb),
                    new TaskPickTargetAroundTransforms(transform, minDistanceFromPlayer,
                        maxDistanceFromPlayer),
                    new TaskMoveToTarget(rb, agent, null, 1f),
                }),
                new Sequence(new List<Node>
                {
                    new ExpectData<bool>(sharedData.HasHeardPlayerShot, true),
                    new SetData<bool>(sharedData.IsAwareOfPlayer, true),
                    new TaskPickTargetAroundTransforms(exitPoint.transform, minDistanceFromPlayer,
                        maxDistanceFromPlayer),
                    new TaskMoveToTarget(rb, agent, null, 1f),
                    new CheckIsAtTarget(),
                    new TaskDeleteGameObject(transform)
                }),
                new SetData<bool>(sharedData.HasHeardPlayerShot, false)
            });
            root.SetupSharedData(sharedData);
            return root;
        }
    }
}