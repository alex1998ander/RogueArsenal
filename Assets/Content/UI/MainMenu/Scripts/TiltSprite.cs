using System;
using UnityEngine;

public class TiltSprite : MonoBehaviour
{
    private float _startRotationX;
    private float _startRotationY;

    private void Start()
    {
        var rotation = transform.rotation.eulerAngles;
        _startRotationX = rotation.x;
        _startRotationY = rotation.y;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 spritePosition = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 direction = mousePosition - spritePosition;

        // Set the rotation directly
        transform.rotation = Quaternion.Euler(new Vector3(_startRotationX + direction.y * 0.005f, _startRotationY - direction.x * 0.01f, 0));
    }
}