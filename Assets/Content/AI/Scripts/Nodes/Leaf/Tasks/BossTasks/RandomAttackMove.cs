using UnityEngine;

namespace BehaviorTree
{
    public class RandomAttackMove: Node
    {
        private Node[] _attacks;

        public RandomAttackMove(Node[] attacks)
        {
            this._attacks = attacks;
        }

        public override NodeState Evaluate()
        {
            return _attacks[sharedData.GetData(sharedData.RandomAbility)].Evaluate();
        }
    }
}