using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Got a Tutorial at: 
    - https://www.youtube.com/watch?v=-VkezxxjsSE&ab_channel=MinaP%C3%AAcheux
     And
    - https://www.youtube.com/watch?v=OtUKsjPWzO8&ab_channel=MinaP%C3%AAcheux
 * Simple Hierarchical State Machine
 */
public class BaseState {

    //Acces the Name from Everywhere
    public string Name;
    public int StateLayer;

    //Need the State Machine only in this class and children Classes.
    protected StateMachine stateMachine;
    protected BaseState _currentSuperState;
    protected BaseState _currentSubState;

    //Getters
    public BaseState SuperState {get {return _currentSuperState;}}
    public BaseState SubState {get {return _currentSubState;}}

    public BaseState(string stateName, StateMachine stateMachine){
        this.Name = stateName;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void InitializeSubState(){}
    //could eventually make a Late Update for things needed to be updated second.
    public virtual void Update(){}


    public void ChangeState(BaseState newState){
        //stateMachine.ChangeState(newState);
        Exit();

        newState.Enter();
        // stateMachine.CurrentState = newState;
    }





    //! REMOVED SUBSTATES AND SUPERSTATES
    //!  - IN favor of an Array Holding the Hierarchy

    // public void UpdateStates(){
    //     Update();
    //     if (_currentSubState != null){
    //         _currentSubState.UpdateStates();
    //     }
    // }
    // protected void SetSuperState(BaseState newSuperState){
    //     _currentSuperState = newSuperState;
    // }
    // protected void SetSubState(BaseState newSubstate){
    //     // if (_currentSubState != null){
    //     //     _currentSubState.Exit();
    //     // }
    //     // _currentSubState = newSubstate;
    //     // newSubstate.Enter();
    //     // newSubstate.SetSuperState(this);
    //     _currentSubState = newSubstate;
    //     stateMachine.StateList[StateLayer + 1] = newSubstate;
    // }
}
