using System;
using BehaviorTree;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacterController
{
    // Behavior tree of this enemy
    private BTree _bTree;

    private void Awake()
    {
        EventManager.OnPlayerShotFired.Subscribe(HearPlayerShotFired);
    }

    private void Start()
    {
        _bTree = GetComponent<BTree>();
    }

    public void StunCharacter()
    {
        _bTree.Stun();
    }

    public void HearPlayerShotFired()
    {
        _bTree.HearShotFired();
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerShotFired.Unsubscribe(HearPlayerShotFired);
    }
}