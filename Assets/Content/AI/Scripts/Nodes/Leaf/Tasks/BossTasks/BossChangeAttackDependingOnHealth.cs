using UnityEngine;

namespace BehaviorTree
{
    public class BossChangeAttackDependingOnHealth: Node
    {
        private Transform _body;
        private float _healthBoundry = 2/3;
        
        public BossChangeAttackDependingOnHealth(Transform body)
        {
            this._body = body;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.SUCCESS;
            Vector2 health = _body.GetComponent<EnemyHealth>().GetHealth();
            if (health.y * _healthBoundry < health.x)
            {
                SetData(sharedData.AbilityPool, sharedData.GetData(sharedData.AbilityPool) + 1);
                _healthBoundry = 1/3;
            } 
            return state;
        }
    }
}