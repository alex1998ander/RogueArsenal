using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitIndicatorController : MonoBehaviour
{
    public GameObject targetIndicator;
    private readonly List<GameObject> _exitIndicators = new List<GameObject>();
    
    void Start()
    {
        int i = 0;
        GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("ExitPoints");
        foreach (GameObject exitPoint in exitPoints)
        {
            Debug.Log(i);
            _exitIndicators.Add(Instantiate(targetIndicator, transform));
            _exitIndicators[i].GetComponent<Targetindicator>().SetTarget(exitPoint.transform);
            i++;
        }
    }

    void Update()
    {
        if (SpawnController.StillEnemiesLeft())
        {
            SetIndicatorsActive(false);
        }
        else
        {
            SetIndicatorsActive(true);
        }
    }
    
    private void SetIndicatorsActive(bool value)
    {
        foreach (GameObject indicator in _exitIndicators)
        {
            indicator.gameObject.SetActive(value);
        }
    }
}
