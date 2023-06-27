using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletWallCheck : MonoBehaviour
{
    //
    private int _borderCount = 2;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            _borderCount--;

            if (_borderCount == 0)
            {
                // Disables that the bullet can shoot through a wall 
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Walls"), LayerMask.NameToLayer("PlayerBullets"), false);
            }
        }
    }
}