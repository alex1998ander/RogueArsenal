using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyAdder : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void AddCurrency()
    {
        for (int i = 0; i < 100; i++)
        {
            ProgressionManager.CollectCurrency();
            EventManager.OnPlayerCollectCurrency.Trigger();
        }
    }
}