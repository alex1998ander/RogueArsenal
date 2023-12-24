using UnityEngine;

public class UpgradeSpawnablePrefabHolder : MonoBehaviour
{
    public static UpgradeSpawnablePrefabHolder instance;

    [Header("Upgrade Prefabs")] [SerializeField]
    public GameObject healingFieldPrefab;

    [SerializeField] public GameObject mentalMeltdownPrefab;
    [SerializeField] public GameObject phoenixPrefab;
    [SerializeField] public GameObject shockwavePrefab;
    [SerializeField] public GameObject stimpackPrefab;
    [SerializeField] public GameObject timefreezePrefab;

    [Header("Other Objects")] [SerializeField]
    public PhysicsMaterial2D bulletBouncePhysicsMaterial;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Spawns an instance of the given prefab at a given position.
    /// </summary>
    /// <param name="prefab">Prefab to spawn</param>
    /// <param name="position">World position to spawn instance</param>
    /// <param name="lifetime">Lifetime of instance</param>
    /// <param name="parentObject">GameObject to parent instance to</param>
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