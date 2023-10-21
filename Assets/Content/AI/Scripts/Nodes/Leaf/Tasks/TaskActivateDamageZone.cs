using UnityEngine;

namespace BehaviorTree
{
    public class TaskActivateDamageZone : Node
    {
        private bool _activate;
        private Collider2D _damageZoneCollider;

        public TaskActivateDamageZone(bool activate, Collider2D damageZoneCollider)
        {
            _activate = activate;
            _damageZoneCollider = damageZoneCollider;
        }

        public override NodeState Evaluate()
        {
            _damageZoneCollider.enabled = _activate;
            state = NodeState.SUCCESS;
            return state;
        }
    }
}