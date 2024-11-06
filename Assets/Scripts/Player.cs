using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
   
    [Tooltip("How Many Units per Second the Player moves")]
    [SerializeField] float movementSpeed = 2;

    public static Player instance;

    Vector2 moveVector;

    Rigidbody2D rigidbody2D;

    InputSystem_Actions inputActions;


    private void Awake()
    {
        if (instance == null && instance != this)
        {
            instance = this;
        }
    }


    void Start()
    {
        InitiateInput();

        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        // Apply movement each FixedUpdate
        rigidbody2D.linearVelocity += moveVector * movementSpeed;
    }

    private void OnDestroy()
    {
        // Disable input actions to prevent memory leaks
        inputActions.Disable();
    }


    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.CompareTag("Deadly"))
        {
            TakeDamage(1);
        }
    }


    private void InitiateInput() // New input system is Amazing ^^
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += HandleMovePerformed;
        inputActions.Player.Move.canceled += HandleMoveCanceled;
    }


    private void HandleMovePerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }


    private void HandleMoveCanceled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;  // Stop movement when the input is released
    }





    private void TakeDamage(int _dmg)
    {
        
    }
}
