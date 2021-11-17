using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonoBehaviour {
    //constants
    static int MAX_STATES = 16;

    BaseState[] _currentStates = new BaseState[MAX_STATES]; 
    BaseState   _rootState;

    //Getter
    public BaseState RootState {get {return _rootState;}}
    public BaseState[] StateList {get {return _currentStates;} set {_currentStates = value;}}  

    void Start() {
        _rootState = GetInitialState();
        _currentStates[1] = _rootState;
        if (_currentStates[1] != null){
            _currentStates[1].Enter();
        }
        Debug.Log("ROOT STATE: " + _rootState);
        
        // currentState = GetInitialState();
        // if (currentState != null){
        //     currentState.Enter();
        // }
    }
    public void ChangeState(int stateLayer, BaseState newState){
        //Exit Current State
        _currentStates[stateLayer].Exit();

        //Set the Variable and State to the New State
        _currentStates[stateLayer] = newState;
        _currentStates[stateLayer].Enter();
    }
    protected virtual BaseState GetInitialState(){
        return null;
    }
    void Update() {
        // START i at 1, indexing from 1
        for (int i = 1; i < MAX_STATES; i++){
            if(_currentStates[i] != null){
                _currentStates[i].Update();
            }
            else{
                i = MAX_STATES;
            }
        }
        // if (_rootState != null){
        //     _rootState.UpdateStates();
        // }
        DoUpdate();
    }
    protected virtual void DoUpdate(){}
}
