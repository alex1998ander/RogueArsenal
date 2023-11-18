using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackShield: Node
    {
        private GameObject _shieldGenerator;
        private Collider2D _bossCollider;
        
        public BossAttackShield(GameObject shieldGenerator, Collider2D bossCollider)
        {
            this._shieldGenerator = shieldGenerator;
            this._bossCollider = bossCollider;
        }

        public override NodeState Evaluate()
        {
            _shieldGenerator.SetActive(true);
            _bossCollider.enabled = false;
            return NodeState.SUCCESS;
        }
    }
}