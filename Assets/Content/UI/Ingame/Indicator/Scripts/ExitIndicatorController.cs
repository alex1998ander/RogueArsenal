using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitIndicatorController : MonoBehaviour
{
    public GameObject targetIndicator;
    private readonly List<GameObject> _exitIndicators = new List<GameObject>();

    void Start()
    {
        StartCoroutine(_DelaySetup());
    }

    // TODO: this is dumb
    private IEnumerator _DelaySetup()
    {
        yield return new WaitForSeconds(1f);
        int i = 0;
        GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("ExitPoints");
        foreach (GameObject exitPoint in exitPoints)
        {
            _exitIndicators.Add(Instantiate(targetIndicator, transform));
            _exitIndicators[i].GetComponent<TargetIndicator>().SetTarget(exitPoint.transform);
            i++;
        }
    }

    void Update()
    {
        if (SpawnController.CheckEnemiesAlive())
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