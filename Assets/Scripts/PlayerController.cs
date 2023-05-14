using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;
    public Camera cam;

    private Rigidbody2D _rb;
    private InputActions _ia;
    private Vector2 _movementInput;
    private Vector2 _mousePos;

    private void Awake()
    {
        _ia = new InputActions();
        _ia.Player.Movement.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
        _ia.Player.Movement.canceled += ctx => _movementInput = Vector2.zero;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 curMovement = MovementSpeed * Time.fixedDeltaTime * _movementInput;
        _rb.velocity = curMovement;

        Vector2 lookDir = _mousePos - _rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }

    private void OnEnable()
    {
        _ia.Enable();
    }

    private void OnDisable()
    {
        _ia.Disable();
    }
}