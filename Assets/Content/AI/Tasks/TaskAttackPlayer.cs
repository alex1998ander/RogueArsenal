using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttackPlayer : Node
{
    private EnemyWeapon _weapon;

    public TaskAttackPlayer(EnemyWeapon weapon) : base()
    {
        _weapon = weapon;
    }

    public override NodeState Evaluate()
    {
        _weapon.Fire();
        state = NodeState.SUCCESS;
        return state;
    }
}