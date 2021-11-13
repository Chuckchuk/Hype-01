using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // Public Variables
    public float WalkSpeedForward = 1f;
    public float WalkSpeedBackward = 1f;
    public float GravityForce = 1f;

    // Reference Variables
    private CharacterController _controller;
    private PlayerInput _playerInput;

    // Store Input Values
    private Vector3 _currentMovement;

    //State Variables
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;

    void Awake() {
        _playerInput = new PlayerInput();
        _controller = GetComponent<CharacterController>();

        //Set up the States
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _playerInput.CharacterControls.Move.performed += context => { OnMoveForward(context.ReadValue<float>()); };
        _playerInput.CharacterControls.Move.canceled += context => { OnMoveForward(context.ReadValue<float>()); };
    }

    void OnEnable() {
        // Enable Character Controls Action Map
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable() {
        // Disable Character Controls Action Map
        _playerInput.CharacterControls.Disable();
    }


     // Takes in the X and Z input and translates it to Movement
    void OnMoveForward(float direction) {
        // _currentMovement.z = direction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
