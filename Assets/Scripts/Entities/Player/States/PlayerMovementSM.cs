using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovementSM : StateMachine {
    //Public Variable Fields
    public float walk_speed_forward = 1f;
    public float walk_speed_backward = 0.6f;
    public float force_gravity = 1f;

    public Text[] _debugText;

    // State Variables
    // * Level 1 States
    private PlayerGrounded  _groundedState;
    private PlayerJump      _jumpState;
    private PlayerFall      _fallState;
    // * Level 2 States
    private BaseState _currentLvl2State;
    private PlayerIdle      _idleState;
    private PlayerWalk      _walkState;
    private PlayerRun       _runState;
    

    // Type Variables
    PlayerInput _playerInput;
    CharacterController _charController;

    // Storage Variables
    Vector2 _movementAxis;
    Vector3 _currentMovement;

    // Flags
    bool _isMovementPressed;
    bool _isJumpPressed;
    bool _isRunPressed;

    // Getters and Setters
    public PlayerGrounded GroundedState {get {return _groundedState;}}
    public PlayerJump JumpState         {get {return _jumpState;}}
    public PlayerFall FallState         {get {return _fallState;}}

    public PlayerIdle IdleState         {get {return _idleState;}}
    public PlayerWalk WalkState         {get {return _walkState;}}
    public PlayerRun  RunState          {get {return _runState;}}

    public BaseState CurrentLvl2State   {get {return _currentLvl2State;}}

    public Vector2 MovementAxis     {get {return _movementAxis;}}
    public Vector3 CurrentMovement  {get {return _currentMovement;}    set {_currentMovement = value;}}
    public float   VerticalMovement {get {return _currentMovement.y;}  set {_currentMovement.y = value;}}


    public bool IsMovementPressed   {get {return _isMovementPressed;}}
    public bool IsJumpPressed       {get {return _isJumpPressed;}}
    public bool IsRunPressed        {get {return _isRunPressed;}}


    private void Awake() {
        // Initialize States
        _groundedState  = new PlayerGrounded(this, 1);
        _jumpState      = new PlayerJump(this, 1);
        _fallState      = new PlayerFall(this, 1);

        _idleState      = new PlayerIdle(this, 2);
        _walkState      = new PlayerWalk(this, 2);
        _runState       = new PlayerRun(this, 2);

        //Handle Player Input
        _playerInput = new PlayerInput();
        _charController = GetComponent<CharacterController>();

        _playerInput.CharacterControls.Move.performed += context => { OnMove(context); };
        _playerInput.CharacterControls.Move.canceled += context => { OnMove(context); };
    }

    private void OnMove(InputAction.CallbackContext context){
        //Store the X Z Movement Axis into _movementAxis and _currentMovement
        _movementAxis = context.ReadValue<Vector2>();
        _currentMovement.x = _movementAxis.x;
        _currentMovement.z = _movementAxis.y;
        //Set Movement Flag
        _isMovementPressed = _movementAxis.x != 0 | _movementAxis.y != 0;
    }

    void OnEnable() {
        // Enable Character Controls Action Map
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable() {
        // Disable Character Controls Action Map
        _playerInput.CharacterControls.Disable();
    }



    /*********************************************
     ************** State Machine ****************
     *********************************************/
    protected override BaseState GetInitialState() {
        return _groundedState;
    }

    protected override void DoUpdate(){
        //Update the SubState Variable
        if (RootState != null && RootState.SubState != null){
            _currentLvl2State = RootState.SubState;
        }
        //Handle Movement and Gravity
        _charController.Move(_currentMovement * Time.deltaTime);
        DebugMenu();
    }

    // Print out Debug Info to the GUI
    private void DebugMenu(){
        string content = StateList[1] != null ? "State: " + StateList[1].Name : "[NO CURRENT STATE]";
        _debugText[0].text = content;
        content = StateList[2] != null ? "-> " + StateList[2].Name : "[NO SUB STATE]";
        _debugText[1].text = content;
    }
}
