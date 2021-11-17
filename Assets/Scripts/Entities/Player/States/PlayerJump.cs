using UnityEngine;

public class PlayerJump : BaseState {
    private PlayerMovementSM _context;
    public PlayerJump(PlayerMovementSM context, int StateLayer) : base("Jump", context) {
        _context = context;
        this.StateLayer = StateLayer;
    }
    public override void Enter(){
        InitializeSubState();

    }
    public override void Exit(){

    }
    /**
     * Jump State:
     ** While Jumping:
     *  - Apply the Same small Force of Gravity as Grounded
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

    }
}
