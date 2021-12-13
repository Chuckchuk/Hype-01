using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : BaseState {
    private PlayerMovementSM _context;

    //// Vector3 _forwardDirection;

    //// BaseState _previousState;

    //// float _speed;
    //// float _acceleration;
    //// float _directionSpeed;

    //// bool _isMovingForward;
    //// bool _isMovingBackward;

    float _initialAccel = 0f;
    float _accelRate    = 0f;
    float _deccelRate   = 0f;
    float _spdForward   = 0f;
    float _spdBackward  = 0f;

    public PlayerWalk(PlayerMovementSM context, int StateLayer) : base("Walk", context) {
        _context = context;
        this.StateLayer = StateLayer;
    }
    public override void Enter(){
        _context.RotationSpeed = _context.WalkRotationSpeed;
        initValues();
    }
    private void initValues(){
        _initialAccel = _context.WalkInitAcceleration;
        _accelRate    = _context.WalkAccelerationRate;
        _deccelRate   = _context.WalkDeccelerationRate;
        _spdForward   = _context.WalkSpeedForward;
        _spdBackward  = _context.WalkSpeedBackward;
    }
    public override void Exit(){

    }

    /**
     * Walk State:
     ** General Movement:
     *  - Move Forward
     *  * While Grounded:
     *    - Accelerate into a Move, then Decellerate upon no longer moving
     *  * While Falling:
     *    -
     *  * While Jumping:
     *    -
     ** SubStates:
     *  <NONE>
     ** Switch State To:
     *  - Running:
     *      When Run Key is Pressed
     *  - Idle:
     *      When:
     *      1. No Movement Keys Pressed
     *      2. Decelerated to a Stop from Walk Speed
    */
    public override void Update(){
        _context.HandleMovementXZ(_initialAccel, _accelRate, _deccelRate, _spdForward, _spdBackward);
        SwitchState();
    }

    // private Vector3 HandleMovement(){
    //     //Accelerate/Deccelerate into/out-of a Walk
    //     if(_context.IsMovementPressed && _context.Acceleration < 1){
    //         _acceleration += _context.WalkAccelerationRate * Time.deltaTime;
    //     }
    //     else if (!_context.IsMovementPressed && _context.Acceleration > 0){
    //         _acceleration -= _context.WalkDeccelerationRate * Time.deltaTime;
    //     }
    //     else if(_context.Acceleration > 1) {
    //         _acceleration = 1;
    //     }
    //     else if (_context.Acceleration < 0) {
    //         _acceleration = 0;
    //     }
    //     _context.Acceleration = _acceleration;

    //     //Set the Movement Vector for X and Z
    //     // Movement is the product of:
    //     //  Acceleration  (multiplies by acceleration amount) [0 to 1]
    //     //  Speed of Direction (Forward vs Backwards Speed for Walking)
    //     //  Direction (Forward or Backwards) [1 or -1]
    //     //  Forward Vector for X and for Z (for rotational independent forward movement)
    //     _forwardDirection = _context.CharController.transform.forward;
    //     if(_context.MovementAxis.y > 0) {
    //         _directionSpeed = _context.WalkSpeedForward * _context.MovementAxis.y;
    //         _isMovingForward = true;
    //         _isMovingBackward = false;
    //     }
    //     else if (_context.MovementAxis.y < 0) {
    //         _directionSpeed = _context.WalkSpeedBackward * _context.MovementAxis.y;
    //         _isMovingForward = false;
    //         _isMovingBackward = true;
    //     }

    //     _speed = _acceleration * _directionSpeed;

    //     // _forwardDirection.x *= _speed;
    //     // _forwardDirection.z *= _speed;
    
    //     return _forwardDirection * _speed;
    // }

    private void SwitchState(){
        //* Switch to Idle or Run:
        if (!_context.IsMovementPressed && _context.Acceleration == 0) {
            _context.ChangeState(this.StateLayer, _context.IdleState);
        }
        else if (_context.IsRunPressed) {
            _context.ChangeState(this.StateLayer, _context.RunState);
        }
    }
}

