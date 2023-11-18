using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SandboxTargetUpgradeSelector : MonoBehaviour
{
    [HideInInspector] [SerializeField] public int selectedIndex;

    [SerializeField] private TMP_Text upgradeText;

    private bool _upgradeSelected;

    private void Start()
    {
        upgradeText.text = UpgradeManager.DefaultUpgradePool[selectedIndex].Name;
        if (selectedIndex % 2 == 0)
        {
            upgradeText.transform.position += Vector3.down * 1.2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_upgradeSelected && other.CompareTag("PlayerBullet"))
        {
            _upgradeSelected = true;
            Debug.Log("Upgrade added: " + UpgradeManager.DefaultUpgradePool[selectedIndex].Name);
            UpgradeManager.BindUpgrade_Sandbox(selectedIndex);
            Collider2D collider2D = GetComponent<Collider2D>();
            collider2D.enabled = false;
            Destroy(gameObject);
            
            PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.InitUpgrades();
            EventManager.OnUpgradeChange.Trigger();
        }
    }
}