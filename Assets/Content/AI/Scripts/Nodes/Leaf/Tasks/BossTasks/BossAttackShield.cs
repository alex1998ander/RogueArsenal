using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackShield : Node
    {
        private GameObject _shieldGenerator;
        private NodeState _state;

        public BossAttackShield(GameObject shieldGenerator)
        {
            this._shieldGenerator = shieldGenerator;
        }

        public override NodeState Evaluate()
        {
            _state = NodeState.RUNNING;
            _shieldGenerator.SetActive(true);
            if (!_shieldGenerator.activeSelf)
            {
                _state = NodeState.SUCCESS;
            }

            return _state;
        }
    }
}