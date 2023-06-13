using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttackPlayer : Node
{
    private Weapon _weapon;

    public TaskAttackPlayer(Weapon weapon) : base()
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