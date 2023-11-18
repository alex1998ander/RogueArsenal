using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackShockwave: Node
    {
        private GameObject _shockwave;
        public BossAttackShockwave(GameObject shockwave)
        {
            this._shockwave = shockwave;
        }

        public override NodeState Evaluate()
        {
            _shockwave.SetActive(true);
            return NodeState.SUCCESS;
        }
    }
}