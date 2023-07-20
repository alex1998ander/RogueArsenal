using BehaviorTree;

/// <summary>
/// Task to attack the player with the enemy's weapon
/// </summary>
public class TaskAttackPlayer : Node
{
    // The weapon of the enemy
    private EnemyWeapon _weapon;

    public TaskAttackPlayer(EnemyWeapon weapon)
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