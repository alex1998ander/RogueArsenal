using UnityEngine;

public class CurrencyDropController : MonoBehaviour
{
    [SerializeField] private GameObject currencyPrefab;

    private const int DropAmount = 5;

    private void Start()
    {
        EventManager.OnEnemyDeath.Subscribe(DropCurrency);
    }

    private void DropCurrency(Vector3 position)
    {
        for (int i = 0; i < DropAmount; i++)
        {
            Instantiate(currencyPrefab, position, Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        EventManager.OnEnemyDeath.Unsubscribe(DropCurrency);
    }
}