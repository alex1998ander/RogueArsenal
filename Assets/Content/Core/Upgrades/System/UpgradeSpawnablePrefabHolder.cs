using UnityEngine;

public class UpgradeSpawnablePrefabHolder : MonoBehaviour
{
    public static UpgradeSpawnablePrefabHolder instance;

    [Header("Upgrade Prefabs")] [SerializeField]
    public GameObject healingFieldPrefab;

    [SerializeField] public GameObject mentalMeltdownPrefab;
    [SerializeField] public GameObject shockwavePrefab;
    [SerializeField] public GameObject stimpackPrefab;
    [SerializeField] public GameObject phoenixPrefab;

    [Header("Other Objects")] [SerializeField]
    public PhysicsMaterial2D bulletBouncePhysicsMaterial;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="lifetime"></param>
    /// <param name="parentObject"></param>
    public static void SpawnPrefab(GameObject prefab, Vector3 position, float lifetime, GameObject parentObject = null)
    {
        GameObject prefabInstance = Instantiate(prefab, position, Quaternion.identity);
        Destroy(prefabInstance, lifetime);

        if (parentObject)
        {
            prefabInstance.transform.SetParent(parentObject.transform, true);
        }
    }
}