using UnityEngine;

public class CurrencyDropController : MonoBehaviour
{
    [SerializeField] private GameObject currencyPrefab;

    private const int DropAmount = 5;

    private void OnDestroy()
    {
        for (int i = 0; i < DropAmount; i++)
        {
            Instantiate(currencyPrefab, transform.position, Quaternion.identity);
        }
    }
}