using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShieldGenerator : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    public GameObject shield;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startColor = Color.blue;
        _lineRenderer.endColor = Color.blue;
        _lineRenderer.enabled = true;
        _lineRenderer.SetPositions(new[] { transform.position, new Vector3(1,1,1) });
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        //shield.SetActive(true);
        shield = GameObject.Find("Shield");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        shield.SetActive(false);
    }
}
