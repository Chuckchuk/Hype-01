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
        Debug.Log("Entered Idle State");
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
        //Switch States Section
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
