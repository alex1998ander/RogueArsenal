using UnityEngine;

namespace BehaviorTree
{
    public class CheckIfPlayerIsInRange : Node
    {
        // Rigidbody of the enemy
        private Rigidbody2D _rb;

        // Transform of the player
        private Transform _playerTransform;

        // Allowed distance to the player before reacting
        private float _viewDistance;

        public CheckIfPlayerIsInRange(Rigidbody2D rb, Transform playerTransform, float viewDistance)
        {
            _rb = rb;
            _playerTransform = playerTransform;
            _viewDistance = viewDistance;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            Vector2 enemyPosition = _rb.position;
            if (_viewDistance >= Vector2.Distance(enemyPosition, _playerTransform.position))
            {
                state = NodeState.SUCCESS;
            }

            return state;
        }
    }
}