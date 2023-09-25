using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

[RequireComponent(typeof(LineRenderer))]
public class BossAttackLaserFocus : MonoBehaviour, IBossAttack
{
    [SerializeField] private Transform focusTarget;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
        _lineRenderer.enabled = false;
    }

    public void ExecuteAbility()
    {
        StartCoroutine(FocusLaser());
    }

    private IEnumerator FocusLaser()
    {

        Vector3 laserStart = transform.position;
        Vector3 focusPos = focusTarget.position;
        Vector3 laserEnd = focusPos + (focusPos - laserStart) * 3f;
        
        _lineRenderer.enabled = true;
        _lineRenderer.SetPositions(new []{laserStart, laserEnd});
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        
        yield return new WaitForSeconds(1f);
        
        _lineRenderer.startWidth = 0.4f;
        _lineRenderer.endWidth = 0.4f;
        
        yield return new WaitForSeconds(2f);

        _lineRenderer.enabled = false;
    }
}