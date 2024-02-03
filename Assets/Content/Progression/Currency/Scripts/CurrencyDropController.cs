using UnityEngine;

public class CurrencyDropController : MonoBehaviour
{
    [SerializeField] private GameObject currencyPrefab;

    private void Start()
    {
        EventManager.OnEnemyCurrencyDropped.Subscribe(DropCurrency);
    }

    private void DropCurrency(Vector3 position, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(currencyPrefab, position, Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        EventManager.OnEnemyCurrencyDropped.Unsubscribe(DropCurrency);
    }
}