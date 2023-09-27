using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;

    private Slider healthbarSlider;

    private void Start()
    {
        healthbarSlider = GetComponent<Slider>();
    }

    void Update()
    {
        Vector2 health = _enemyHealth.GetHealth();
        float healthInPercent = health[0] / health[1];
        healthbarSlider.value = healthInPercent;
    }
}