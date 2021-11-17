using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : BaseState {
    private PlayerMovementSM _context;
    public PlayerWalk(PlayerMovementSM context, int StateLayer) : base("Walk", context) {
        _context = context;
        this.StateLayer = StateLayer;
    }
    public override void Enter(){
        Debug.Log("Entered Walk State");
    }
    public override void Exit(){

    }
    /**
     * Walk State:
     ** General Movement:
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
     *  - Running:
     *      When Run Key is Pressed
     *  - Idle:
     *      When:
     *      1. No Movement Keys Pressed
     *      2. Decelerated to a Stop from Walk Speed
     *!     Maybe Make a Deceleration State?
    */
    public override void Update(){
        //Switch States Section
        //* Switch to Idle or Run:
        if (!_context.IsMovementPressed) {
            _context.ChangeState(this.StateLayer, _context.IdleState);
        }
        else if (_context.IsRunPressed) {
            _context.ChangeState(this.StateLayer, _context.RunState);
        }
    }
}

