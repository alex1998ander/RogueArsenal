using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour

{
    private float timeToLive = 1f;

    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}