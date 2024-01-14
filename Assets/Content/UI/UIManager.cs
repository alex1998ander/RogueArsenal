using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private static UIManager instance;

    private void Awake()
    {
        instance = this;
    }
}
