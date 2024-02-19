using UnityEngine;

namespace BehaviorTree
{
    public class BossChangeAttackDependingOnHealth : Node
    {
        private EnemyHealth _enemyHealth;

        public BossChangeAttackDependingOnHealth(Transform body)
        {
            _enemyHealth = body.GetComponent<EnemyHealth>();
        }

        public override NodeState Evaluate()
        {
            state = NodeState.SUCCESS;
            Vector2 health = _enemyHealth.GetHealth();

            switch (health.x / health.y)
            {
                case <= 1f / 3f:
                {
                    SetData(sharedData.AbilityPool, 2);
                    break;
                }
                case <= 2f / 3f:
                {
                    SetData(sharedData.AbilityPool, 1);
                    break;
                }
            }

            return state;
        }
    }
}