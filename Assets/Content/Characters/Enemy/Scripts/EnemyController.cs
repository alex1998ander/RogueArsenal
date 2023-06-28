using BehaviorTree;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacterController
{
    // Behavior tree of this enemy
    private BTree _bTree;

    private void Start()
    {
        _bTree = GetComponent<BTree>();
    }

    public void StunCharacter()
    {
        _bTree.Stun();
    }
}