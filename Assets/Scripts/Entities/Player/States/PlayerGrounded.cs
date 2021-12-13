using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : BaseState {
    private PlayerMovementSM _context;
    public PlayerGrounded(PlayerMovementSM context, int StateLayer) : base("Grounded", context) {
        _context = context;
        this.StateLayer = StateLayer;
    }
    public override void Enter(){
        InitializeSubState();
        Debug.Log("Entered Grounded State");
    }
    public override void Exit(){

    }
    /**
    ** SubStates:
     *  - Idle
     *  - Walking
     *  - Running
     */
    public override void InitializeSubState(){
        BaseState SubState = null;
        if (_context.StateList[this.StateLayer + 1] != null) {
            SubState = _context.StateList[this.StateLayer + 1];
        }
        //Idle
        else if (!_context.IsMovementPressed) {
            SubState = _context.IdleState;
        }
        //Run
        else if (_context.IsMovementPressed) {
            if (_context.IsRunPressed){
                SubState = _context.RunState;
            }
            else {
                SubState = _context.WalkState;
            }
        }
        // SetSubState(SubState);
        _context.StateList[this.StateLayer + 1] = SubState;
    }
    /**
     * Grounded State:
     ** While Grounded:
     *  - Apply a small force of gravity. (To stay grounded)
     ** SubStates:
     *  - Idle
     *  - Walking
     *  - Running
     ** Switch State To:
     *  - Falling:
     *      When player is no longer grounded
     *  - Jumping:
     *      When player presses [JUMP] button
    */
    public override void Update(){
        //Debug.Log("SUB STATE: " + SubState);
        _context.VerticalMovement = -0.5f;

        //Change States
        SwitchState();
    }
    private void SwitchState() {
        //* Switch to Falling or Jumping
        //   Fall
        if (!_context.IsGrounded){
            _context.ChangeState(this.StateLayer, _context.FallState);
        }
        //   Jump
        else if(_context.IsJumpPressed){
            _context.ChangeState(this.StateLayer, _context.JumpState);
        }
    }
}
