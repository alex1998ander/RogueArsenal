using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFader : MonoBehaviour
{
    [SerializeField] public float IntensityChange = -1f;
    [SerializeField] public float MinLightIntensity = 0f;
    [SerializeField] public float MaxLightIntensity = 5f;

    private Light2D _light;

    private void Start()
    {
        _light = GetComponent<Light2D>();
    }

    void Update()
    {
        _light.intensity = Mathf.Clamp(_light.intensity + IntensityChange * Time.deltaTime, MinLightIntensity, MaxLightIntensity);
    }
}