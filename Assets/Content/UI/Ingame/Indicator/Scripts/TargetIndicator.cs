using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetIndicator : MonoBehaviour
{
    private Transform _target;
    private const float HideDistance = 3.0f;
    private GameObject[] _exitPoints;

    private void Start()
    {
        _exitPoints = GameObject.FindGameObjectsWithTag("ExitPoints");
    }

    void Update()
    {
        var dir = _target.position - transform.position;

        if (dir.magnitude < HideDistance)
        {
            SetChildrenActive(false);
        }
        else
        {
            SetChildrenActive(true);
            
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }
}
