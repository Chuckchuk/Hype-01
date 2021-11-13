/**
* All As seen on Youtube Tutorial:
*  https://www.youtube.com/watch?v=kV06GiJgFhc&list=PLwyUzJb_FNeQrIxCEjj5AMPwawsw5beAy&index=6&ab_channel=iHeartGameDev
*  - Credit to iHeartGameDev
*/

public abstract class PlayerBaseState
{
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();

    void UpdateStates(){

    }
    void SwitchState(){

    }
    void SetSuperState(){

    }
    void SetSubState(){

    }
}
