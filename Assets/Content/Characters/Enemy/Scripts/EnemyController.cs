using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, ICharacterController
{
    // Behavior tree of this enemy
    private BehaviorTree.BehaviorTree _behaviorTree;

    private void Awake()
    {
        EventManager.OnPlayerShotFired.Subscribe(HearPlayerShotFired);
        
        // Set up nav agent
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        _behaviorTree = GetComponent<BehaviorTree.BehaviorTree>();
    }

    public void StunCharacter()
    {
        _behaviorTree.Stun();
    }

    public void HearPlayerShotFired()
    {
        _behaviorTree.HearShotFired();
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerShotFired.Unsubscribe(HearPlayerShotFired);
    }
}