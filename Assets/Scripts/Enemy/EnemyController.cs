using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int HitPoints;

    public string BulletTag = "Bullet";

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(BulletTag))
        {
            HitPoints--;
            if (HitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}