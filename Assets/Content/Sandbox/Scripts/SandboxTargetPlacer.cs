using UnityEngine;

public class SandboxTargetPlacer : MonoBehaviour
{
    [SerializeField] private GameObject sandboxTargetPrefab;

    [SerializeField] private Vector2 placementDirection;

    void Start()
    {
        for (int i = 0; i < UpgradeManager.DefaultUpgradePool.Count; i++)
        {
            GameObject target = Instantiate(sandboxTargetPrefab);
            target.GetComponent<SandboxTargetUpgradeSelector>().selectedIndex = i;
            target.transform.position = transform.position + i * (Vector3) placementDirection;
        }
    }
}