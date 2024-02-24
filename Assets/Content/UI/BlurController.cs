using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurController : MonoBehaviour
{
    [SerializeField] private PostProcessVolume postProcessVolume;

    public void EnableBlur(bool enabled)
    {
        //postProcessVolume.enabled = enabled;
    }
}