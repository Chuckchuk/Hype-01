using UnityEngine;

public class PlayerFall : BaseState {
    private PlayerMovementSM _context;
    public PlayerFall(PlayerMovementSM context, int StateLayer) : base("Fall", context) {
        _context = context;
        this.StateLayer = StateLayer;
    }
    public override void Enter(){
        InitializeSubState();

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

    }
}
