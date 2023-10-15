using System;
using TMPro;
using UnityEngine;

public class SandboxTargetUpgradeSelector : MonoBehaviour
{
    public WeaponUpgrade weaponUpgrade;
    [HideInInspector] [SerializeField] public int selectedIndex;

    [SerializeField] private TMP_Text upgradeText;

    private bool _upgradeSelected;

    private void Start()
    {
        upgradeText.text = UpgradeManager.DefaultWeaponUpgradePool[selectedIndex].Name;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_upgradeSelected && other.gameObject.CompareTag("PlayerBullet"))
        {
            _upgradeSelected = true;
            Debug.Log("Upgrade added: " + UpgradeManager.DefaultWeaponUpgradePool[selectedIndex].Name);
            UpgradeManager.BindWeaponUpgrade_Sandbox(selectedIndex);
            Collider2D collider2D = GetComponent<Collider2D>();
            collider2D.enabled = false;
            Destroy(gameObject);
        }
    }
}