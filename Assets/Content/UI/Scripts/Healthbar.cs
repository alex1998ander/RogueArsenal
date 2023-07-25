using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbar;
    [SerializeField] private TextMeshProUGUI healthText;
    private PlayerHealth _playerHealth;
    private Vector2 _health;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        _health = _playerHealth.GetHealth();
        healthText.text = _health[0] + "/" + _health[1];
        healthbar.fillAmount = _health[0] / _health[1];
    }
}
