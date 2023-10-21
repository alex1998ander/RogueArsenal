using UnityEngine;

public class UpgradeSpawnablePrefabHolder : MonoBehaviour
{

    public static UpgradeSpawnablePrefabHolder instance;

    [SerializeField] public GameObject healingFieldPrefab;


    private void Awake()
    {
        instance = this;
    }
}