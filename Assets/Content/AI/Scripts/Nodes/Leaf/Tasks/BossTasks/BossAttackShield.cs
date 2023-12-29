using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackShield: Node
    {
        private GameObject _shieldGenerator;
        private Collider2D _bossCollider;
        private NodeState _state;
        
        public BossAttackShield(GameObject shieldGenerator, Collider2D bossCollider)
        {
            this._shieldGenerator = shieldGenerator;
            this._bossCollider = bossCollider;
        }

        public override NodeState Evaluate()
        {
            _state = NodeState.RUNNING;
            _shieldGenerator.SetActive(true);
            _bossCollider.enabled = false;
            if (!_shieldGenerator.activeSelf)
            {
                _state = NodeState.SUCCESS;
            }
            return _state;
        }
    }
}