using UnityEngine;

public class SandboxMutatorView : MonoBehaviour
{
    [SerializeField] private ToggleView invulnerableToggle;
    [SerializeField] private ToggleView unlimitedAmmoToggle;

    private void Awake()
    {
        invulnerableToggle.Initialize(null);
        unlimitedAmmoToggle.Initialize(null);
    }
}
