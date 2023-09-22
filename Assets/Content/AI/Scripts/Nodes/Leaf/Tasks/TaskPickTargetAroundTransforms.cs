using UnityEngine;
using Random = UnityEngine.Random;

namespace BehaviorTree
{
    /// <summary>
    /// Task which lets the enemy pick a position around the player as a pathfinding target
    /// </summary>
    public class TaskPickTargetAroundTransforms : Node
    {
        // Transform of the player
        private readonly Transform[] _targetTransforms;

        private readonly float _minDistanceFromTarget;

        private readonly float _maxDistanceFromTarget;

        public TaskPickTargetAroundTransforms(Transform targetTransform, float minDistanceFromTarget,
            float maxDistanceFromTarget)
        {
            _targetTransforms = new[] {targetTransform};
            _minDistanceFromTarget = minDistanceFromTarget;
            _maxDistanceFromTarget = maxDistanceFromTarget;
        }

        public TaskPickTargetAroundTransforms(Transform[] targetTransforms, float minDistanceFromTarget,
            float maxDistanceFromTarget)
        {
            _targetTransforms = targetTransforms;
            _minDistanceFromTarget = minDistanceFromTarget;
            _maxDistanceFromTarget = maxDistanceFromTarget;
        }

        public override NodeState Evaluate()
        {
            Debug.Log("length: " + _targetTransforms.Length);
            int random = Random.Range(0, _targetTransforms.Length);
            Vector2 randomDirection = Random.insideUnitCircle;
            Vector3 randomPositionAroundPlayer = _targetTransforms[random].position
                                                 + (Vector3) (_minDistanceFromTarget * randomDirection
                                                              + Random.Range(0f, 1f) * _maxDistanceFromTarget *
                                                              randomDirection);

            SetData(sharedData.Target, randomPositionAroundPlayer);

            // Debug.Log("Target: " + _targetTransforms[random].position);

            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }
}