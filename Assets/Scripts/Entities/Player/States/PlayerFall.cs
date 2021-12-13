using UnityEngine;

public class PlayerFall : BaseState {
    private PlayerMovementSM _context;
    float _fallForce = 0f;
    
    public PlayerFall(PlayerMovementSM context, int StateLayer) : base("Fall", context) {
        _context = context;
        this.StateLayer = StateLayer;
    }
    public override void Enter(){
        InitializeSubState();
        _fallForce = 0f;
    }
    public override void Exit(){

    }
    /**
     * Falling State:
     ** While Not Grounded:
     *  - Apply the Gravity Force
     *  - 
     *  - 
     ** SubStates:
     *  - Idle
     *  - Walking
     *  - Running
     ** Switch State To:
     *  - Grounded:
     *      When Player is Grounded
    */
    public override void Update(){
        _context.VerticalMovement = _fallForce;
        if (_fallForce < _context.GravityForce){
            _fallForce -= _context.GravityForce * Time.deltaTime;
        }
        else {
            _fallForce = _context.GravityForce;
        }


        //Switch State
        SwitchState();
    }
    private void SwitchState(){
        //* Switch to Grounded
        //   Grounded
        if (_context.IsGrounded){
            _context.ChangeState(this.StateLayer, _context.GroundedState);
        }
    }
}
