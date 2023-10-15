using UnityEngine;

public class EnemyDropController : MonoBehaviour
{
    private const int DropAmount = 5;
    [SerializeField] private GameObject currencyPrefab;

    private void Start()
    {
        EventManager.OnEnemyDeath.Subscribe(DropCurrency);
    }

    private void DropCurrency(GameObject enemy)
    {
        for (int i = 0; i < DropAmount; i++)
        {
            GameObject drop = Instantiate(currencyPrefab, enemy.transform.position, Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        EventManager.OnEnemyDeath.Unsubscribe(DropCurrency);
    }
}