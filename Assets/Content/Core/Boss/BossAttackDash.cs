using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class BossAttackDash : MonoBehaviour, IBossAttack
{

    [SerializeField] private Transform dashTarget;
    
    public void ExecuteAbility()
    {
        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        Vector2 dashDir = (dashTarget.position - transform.position);
        
        // Charge dash
        yield return new WaitForSeconds(1f);
        
        // Launch dash
        GetComponent<Rigidbody2D>().AddForce(dashDir * 1500f );
        
    }
}