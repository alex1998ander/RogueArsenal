using UnityEngine;

/// <summary>
/// Delegator script to make playing particle systems possible through animations
/// </summary>
public class ParticleSystemPlayDelegator : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    public void PlayParticleSystem()
    {
        ps.Play();
    }
}