using UnityEngine;

namespace BehaviorTree
{
    public class ChooseRandomAttackMove: Node
    {
        private int _taskLength;

        public ChooseRandomAttackMove(int taskLength)
        {
            this._taskLength = taskLength;
        }
        public override NodeState Evaluate()
        {
            int rand = Random.Range(0, _taskLength);
            while(sharedData.GetData(sharedData.RandomAbility) == rand)
            {
                rand = Random.Range(0, _taskLength);
            }
            SetData(sharedData.RandomAbility, rand);
            return NodeState.SUCCESS;
        }
    }
}