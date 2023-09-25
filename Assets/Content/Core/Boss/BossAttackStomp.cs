using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class BossAttackStomp : MonoBehaviour, IBossAttack
{

    [SerializeField] private Transform stompTarget;
    [SerializeField] private SpriteRenderer bossVisual;

    public void ExecuteAbility()
    {
        StartCoroutine(Stomp());
    }

    private IEnumerator Stomp()
    {
        // Jump
        bossVisual.enabled = false;
        yield return new WaitForSeconds(1.5f);

        Vector3 landPos = stompTarget.position;
        yield return new WaitForSeconds(1.5f);

        bossVisual.enabled = true;
        transform.position = landPos;
    }
}