using UnityEngine;

public class PlayerJump : BaseState {
    private PlayerMovementSM _context;

    float _initialForce = 0f;
    float _gravityForce = 0f;
    float _currentForce = 0f;

    public PlayerJump(PlayerMovementSM context, int StateLayer) : base("Jump", context) {
        _context = context;
        this.StateLayer = StateLayer;
    }
    public override void Enter(){
        InitializeSubState();
        _initialForce = _context.JumpForce;
        _currentForce = _initialForce;
        _gravityForce = _context.GravityForce;

    }
    public override void Exit(){

    }
    /**
     * Jump State:
     ** While Jumping:
     *  - Apply a Gravity Force, which acts as deceleration.
     *  - 
     *  -
     ** SubStates:
     *  - Idle
     *  - Walking
     *  - Running
     ** Switch State To:
     *  - Falling:
     *      When Jump Height is reached
     *  - Grounded:
     *      If Player is Somehow Grounded while Jumping
    */
    public override void Update(){
        //Jump and apply the Gravity Force to the Jump
        _context.VerticalMovement = _currentForce;
        if(_currentForce >= 0){
            _currentForce -= _gravityForce * Time.deltaTime;
        }
        SwitchState();
    }

    private void SwitchState() {
        //* Switch to Falling or Jumping
        //   Fall
        if (_currentForce <= 0){
            _context.ChangeState(this.StateLayer, _context.FallState);
        }
        //   Grounded
        // else if(_context.IsGrounded && _currentForce < _initialForce){
        //     _context.ChangeState(this.StateLayer, _context.GroundedState);
        // }
    }
}
