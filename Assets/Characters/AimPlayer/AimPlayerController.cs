using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class AimPlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;

    private Vector2 _mousePosition;
    private Vector2 _movementInput;
    private bool _isMouse;
    private Vector3 _aimDirection;
    private float _angle;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (_movementInput * (moveSpeed * Time.fixedDeltaTime)));
    }
    
    private void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
    }

    private void OnFire()
    {
        weapon.Fire();
    }

    private void OnAim(InputValue value)
    {
        _aimDirection = value.Get<Vector2>();
        if (Vector2.Distance(Vector2.zero, _aimDirection) > 0.5)
        {
            if (playerInput.currentControlScheme.Equals("Keyboard&Mouse"))
            {
                _aimDirection = (Vector2)Camera.main.ScreenToWorldPoint(_aimDirection) - rb.position;
            }
            _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg - 90;
            rb.rotation = _angle;
            // Debug.Log(_angle);
        }
        
    }
}
