using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : BaseState {
    private PlayerMovementSM _context;

    float _initialAccel = 0f;
    float _accelRate    = 0f;
    float _deccelRate   = 0f;
    float _spdForward   = 0f;
    //float _spdBackward  = 0f;

    public PlayerRun(PlayerMovementSM context, int StateLayer) : base("Run", context) {
        _context = context;
        this.StateLayer = StateLayer;
    }
    public override void Enter(){
        _context.RotationSpeed = _context.RunRotationSpeed;

        initValues();
    }
    private void initValues(){
        _initialAccel = _context.RunInitAcceleration;
        _accelRate    = _context.RunAccelerationRate;
        _deccelRate   = _context.RunDeccelerationRate;
        _spdForward   = _context.RunSpeedForward;
        //_spdBackward  = _context.RunSpeedBackward;
    }
    public override void Exit(){

    }
    public override void Update(){
        //! Movement: No backward Speed for Running Yet
        _context.HandleMovementXZ(_initialAccel, _accelRate, _deccelRate, _spdForward, 0f);
        SwitchState();
    }

    private void SwitchState(){
        //* Switch to Idle or Run:
        if (!_context.IsMovementPressed && _context.Acceleration == 0) {
            _context.ChangeState(this.StateLayer, _context.IdleState);
        }
        else if (!_context.IsRunPressed) {
            _context.ChangeState(this.StateLayer, _context.WalkState);
        }
    }
}
