using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControlls _controls;
    private CharacterController _characterController;

    [Header("Movement Info")]
    [SerializeField]private float _walkSpeed;
    private Vector3 movementDirection;

    private Vector2 _moveInput;
    private Vector2 _aimInput;

    private void Awake()
    {
        _controls = new PlayerControlls();

        _controls.Character.Movement.performed += context => _moveInput = context.ReadValue<Vector2>();
        _controls.Character.Movement.canceled += context => _moveInput = Vector2.zero;

        _controls.Character.Aim.performed += context => _aimInput = context.ReadValue<Vector2>();
        _controls.Character.Aim.canceled += context => _aimInput = Vector2.zero;

    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        movementDirection = new Vector3(_moveInput.x, 0, _moveInput.y);

        if (movementDirection.magnitude > 0)
        {
            _characterController.Move(movementDirection * Time.deltaTime * _walkSpeed);
        }
    }

    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }
}
