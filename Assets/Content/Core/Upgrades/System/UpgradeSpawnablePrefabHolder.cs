using UnityEngine;

public class UpgradeSpawnablePrefabHolder : MonoBehaviour
{
    public static UpgradeSpawnablePrefabHolder instance;

    [Header("Upgrade Prefabs")]
    [SerializeField] public GameObject healingFieldPrefab;
    [SerializeField] public GameObject shockwavePrefab;
    [SerializeField] public GameObject phoenixPrefab;

    [Header("Other Objects")]
    [SerializeField] public PhysicsMaterial2D bulletBouncePhysicsMaterial;

    private void Awake()
    {
        instance = this;
    }
}