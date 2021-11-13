/**
* All As seen on Youtube Tutorial:
*  https://www.youtube.com/watch?v=kV06GiJgFhc&list=PLwyUzJb_FNeQrIxCEjj5AMPwawsw5beAy&index=6&ab_channel=iHeartGameDev
*  - Credit to iHeartGameDev
*/
public class PlayerStateFactory{
    PlayerStateMachine _context;
    public PlayerStateFactory(PlayerStateMachine currentContext){
        _context = currentContext;
    }

    public PlayerBaseState Idle(){
        return new PlayerIdleState();
    }
    public PlayerBaseState Walk(){
        return new PlayerWalkState();
    }
    // public PlayerBaseState Run(){
    //     return new PlayerRunState();
    // }
    // public PlayerBaseState Jump(){
    //     return new PlayerJumpState();
    // }
    public PlayerBaseState Grounded(){
        return new PlayerGroundedState();
    }

}
