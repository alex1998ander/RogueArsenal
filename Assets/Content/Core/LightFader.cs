using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFader : MonoBehaviour
{
    [SerializeField] private float intensityLossPerSecond = 1f;

    private Light2D _light;

    private void Start()
    {
        _light = GetComponent<Light2D>();
    }

    void Update()
    {
        _light.intensity = Mathf.Max(0, _light.intensity - Time.deltaTime * intensityLossPerSecond);
    }
}