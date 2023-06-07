using BehaviorTree;

public class EnemyBT : BTree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;

    protected override Node SetupTree()
    {
        Node root = new TaskPatrol(transform, waypoints);

        return root;
    }
}