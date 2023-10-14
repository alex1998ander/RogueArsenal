using System;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SandboxTargetUpgradeSelector : MonoBehaviour
{
    public WeaponUpgrade weaponUpgrade;
    [HideInInspector] [SerializeField] public int selectedIndex;

    [SerializeField] private TMP_Text upgradeText;

    private void Start()
    {
        upgradeText.text = UpgradeManager.DefaultWeaponUpgradePool[selectedIndex].Name;
    }

    private void OnCollisionEnter(Collision other)
    {
    }
}