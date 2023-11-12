using UnityEngine;

public class UpgradeSpawnablePrefabHolder : MonoBehaviour
{
    public static UpgradeSpawnablePrefabHolder instance;

    [SerializeField] public GameObject healingFieldPrefab;
    [SerializeField] public GameObject shockwavePrefab;
    [SerializeField] public PhysicsMaterial2D bulletBouncePhysicsMaterial;

    private void Awake()
    {
        instance = this;
    }
}