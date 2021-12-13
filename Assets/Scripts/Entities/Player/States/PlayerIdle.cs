using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : BaseState {
    private PlayerMovementSM _context;
    
    public PlayerIdle(PlayerMovementSM context, int StateLayer) : base("Idle", context) {
        _context = context;
        this.StateLayer = StateLayer;
    }
    public override void Enter(){
        ////Debug.Log("Entered Idle State");
        _context.RotationSpeed = _context.IdleRotationSpeed;


        // _context.AppliedVectorX = 0;
        // _context.AppliedVectorZ = 0;

    }
    public override void Exit(){

    }
    /**
     * Idle State:
     ** General Idle:
     *  - 
     *  * While Grounded:
     *    -
     *  * While Falling:
     *    -
     *  * While Jumping:
     *    -
     ** SubStates:
     *  <NONE>
     ** Switch State To:
     *  - Walking:
            When Walk is pressed [W, A, S, D]
     *  - Running:
     *      When Run Key AND walk is Pressed
    */
    public override void Update(){
        //////Handle Walking Camera
        ////_context.CamMove.HandleIdleCamera();
        //// if(_context.Acceleration < 1){
        ////     _context.Acceleration -= _context.IdleDeccelerationRate * Time.deltaTime;
        //// }
        //// if(_context.Acceleration > 1){
        ////     _context.Acceleration = 1;
        //// }

        ////HandleRotation();

        //Switch States
        SwitchState();
    }

    //// private void HandleRotation(){
    ////     CharacterController charControl = _context.CharController;
    ////     charControl.transform.Rotate(0, _context.RotationInput * Time.deltaTime, 0);
    //// }

    private void SwitchState(){
        //* Switch to Walk or Run:
        if (_context.IsMovementPressed){
            //* Switch to Running State
            //  - When Run Key AND walk is Pressed
            if (_context.IsRunPressed){
                _context.ChangeState(this.StateLayer, _context.RunState);
            }
            //* Switch to Walking State
            //  - When Walk is Pressed [W, A, S, D] 
            else {
                _context.ChangeState(this.StateLayer, _context.WalkState);
            }
        }
    }
}
