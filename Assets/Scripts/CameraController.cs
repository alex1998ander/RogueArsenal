using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform;

    private void Update()
    {
        transform.position =
            new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, transform.position.z);
    }
}