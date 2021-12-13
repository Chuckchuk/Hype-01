using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerMovementSM : StateMachine {
    //Public Variable Fields
    //Walk Speeds
    [Header("Force Speeds")]
    public float WalkSpeedForward = 1f;
    public float WalkSpeedBackward = 0.6f;
    //Run Speed
    public float RunSpeedForward = 1f;
    public float GravityForce = 9f;
    public float JumpForce = 10f;

    [Header("Rotation")]
    public float IdleRotationSpeed = 1f;
    public float WalkRotationSpeed = 0.8f;
    public float RunRotationSpeed = 0.5f;
    
    [Header("Walk Acceleration")]
    public float WalkInitAcceleration = 0.2f;
    public float WalkAccelerationRate = 1f;
    public float WalkDeccelerationRate = 1f;
    [Header("Run Acceleration")]
    public float RunInitAcceleration = 0.2f;
    public float RunAccelerationRate = 1f;
    public float RunDeccelerationRate = 1f;



    [Space(10)]
    [Header("Attack")]
    public float AttackSpeed;
    public float ProjectileSpeed;
    public GameObject Projectile;

    // public float IdleDeccelerationRate = 0.5f;

    [Space(5)]
    public Camera Camera;

    public Text[] _debugText;


    //CameraMovement Script
    public CameraMovement CamMove;

    // State Variables
    // * Level 1 States
    private PlayerGrounded  _groundedState;
    private PlayerJump      _jumpState;
    private PlayerFall      _fallState;
    // * Level 2 States
    // // private BaseState _currentLvl2State;
    private PlayerIdle      _idleState;
    private PlayerWalk      _walkState;
    private PlayerRun       _runState;
    

    // Type Variables
    PlayerInput _playerInput;
    CharacterController _charController;

    // Transform Variables
    Vector2 _movementAxis;
    float   _rotationInput;
    Vector3 _currentMovement;
    Vector3 _currentMovementForward;
    float _rotationSpeed = 1f;
    float _acceleration;
    float _directionSpeed;
    Vector2 _mousePos;

    // Applied Movement Variables
    Vector3 _appliedMovement;
    Vector3 _appliedVectorXZ;
    float _appliedRotationY;

    // Flags
    bool _isMovementPressed;
    bool _isPlayerGrounded;
    bool _isJumpPressed;
    bool _isRunPressed;

    // Getters and Setters
    public PlayerGrounded GroundedState {get {return _groundedState;}}
    public PlayerJump JumpState         {get {return _jumpState;}}
    public PlayerFall FallState         {get {return _fallState;}}

    public PlayerIdle IdleState         {get {return _idleState;}}
    public PlayerWalk WalkState         {get {return _walkState;}}
    public PlayerRun  RunState          {get {return _runState;}}

    // // public BaseState CurrentLvl2State   {get {return _currentLvl2State;}}

    public PlayerInput PInput                   {get {return _playerInput;}}   
    public CharacterController CharController   {get {return _charController;}}   


    public Vector2 MovementAxis     {get {return _movementAxis;}}
    public float   RotationInput    {get {return _rotationInput;}}
    public Vector3 CurrentMovement  {get {return _currentMovement;}     set {_currentMovement = value;}}
    public float   VerticalMovement {get {return _currentMovement.y;}   set {_currentMovement.y = value;}}
    public Vector2 MousePos         {get {return _mousePos;}}

    public float RotationSpeed      {get {return _rotationSpeed;}       set {_rotationSpeed = value;}}
    public float Acceleration       {get {return _acceleration;}        set {_acceleration = value;}}
    // Contains the Motion (speed) information from the Individual states to be given 

    ////public Vector3 AppliedMovement  {get {return _appliedMovement;}}
    //////Get the XZ Vector
    ////public float AppliedVectorX     {get {return _appliedVectorXZ.x;}        set {_appliedVectorXZ.x = value;}}
    ////public float AppliedVectorZ     {get {return _appliedVectorXZ.z;}        set {_appliedVectorXZ.z = value;}}
    public float AppliedRotationY   {get {return _appliedRotationY;}}

    public bool IsMovementPressed   {get {return _isMovementPressed;}}
    public bool IsGrounded          {get {return _isPlayerGrounded;}}
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

        //Player Camera Script
        CamMove = new CameraMovement(this);

        // Variable Initializations
        _rotationSpeed = IdleRotationSpeed;

        //Player Input
        // _playerInput.CharacterControls.Move.started += context => { OnMove(context); };
        _playerInput.CharacterControls.Move.performed += context => { OnMove(context); };
        _playerInput.CharacterControls.Move.canceled += context => { OnMove(context); };

        _playerInput.CharacterControls.Run.performed += context => { OnRun(context); };
        _playerInput.CharacterControls.Run.canceled += context => { OnRun(context); };
        
        _playerInput.CharacterControls.Jump.performed += context => { OnJump(context); };
        _playerInput.CharacterControls.Jump.canceled += context => { OnJump(context); };

        _playerInput.CharacterControls.Attack.performed += context => { OnAttack(context); };

        _playerInput.CharacterControls.Quit.performed += context => { QuitGame(); };

        //// _playerInput.CharacterControls.Rotate.performed += context => { OnRotate(context); };
        //// _playerInput.CharacterControls.Rotate.canceled += context => { OnRotate(context); };

    }

    private void OnMove(InputAction.CallbackContext context){
        //Store the X Z Movement Axis into _movementAxis and _currentMovement
        _movementAxis = context.ReadValue<Vector2>();
        // //_currentMovement.x = _movementAxis.x;
        _rotationInput = _movementAxis.x;
        
        ////_currentMovement.z = _movementAxis.y;
        //Set Movement Flag
        // //_isMovementPressed = _movementAxis.x != 0 | _movementAxis.y != 0;
        _isMovementPressed = _movementAxis.y != 0;

        //Debug.Log(context.ReadValue<Vector2>());
    }
    private void OnRun(InputAction.CallbackContext context) {
        _isRunPressed = context.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext context) {
        _isJumpPressed = context.ReadValueAsButton();
    }

    /**
     * On Attack
     *  Simply write most of the logic here:
     *    - When player attacks, create a new GameObject (Sword Projectile)
     *    - Make the Object fly forward for some distance
     */
    private void OnAttack(InputAction.CallbackContext context) {
        Vector3 attackCenter = new Vector3(0, 0.2f, 0);
        GameObject projectile = Instantiate(Projectile, transform.position + attackCenter + (transform.forward * 1.2f), Quaternion.identity);
        Quaternion swingOrientation = transform.localRotation;
        // swingOrientation *= Quaternion.Euler(-90, 0, -90);
        projectile.transform.rotation = swingOrientation;
    }

    private void QuitGame(){
        Debug.Log("Quit Game");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
 
    // //private void OnRotate(InputAction.CallbackContext context){
    // //    _rotationInput = context.ReadValue<float>();
    // //}
 
    void OnEnable() {
        // Enable Character Controls Action Map
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable() {
        // Disable Character Controls Action Map
        _playerInput.CharacterControls.Disable();
    }



    /*********************************************
     ************ General Movement ***************
     *********************************************/
    private void HandleGravity() {
        //Update the IsGrounded Bool
        _isPlayerGrounded = _charController.isGrounded;
        
    }
    /**
     ** Handle Move
     *  - Handle the Move of the Character Controller, based
     *  on the _currentMovement variable which is handled in:
     *    + HandleMovementXZ
     *!    + HandleMovementY
     */
    private void HandleMove(){
        _appliedMovement = _currentMovement * Time.deltaTime;
        _charController.Move(_appliedMovement);
    }
    /**
     ** Handle Movement XZ
     *  - This function is called by the Individual Movement States:
     *      + Walk
     *      + Run
     */
    public void HandleMovementXZ(float initAccel, float accelerationRate, float deccelerationRate, float forwardSpeed, float backwardSpeed){
         //Accelerate/Deccelerate into/out-of a Walk
        if(_isMovementPressed && _acceleration < 1){
            //Set Initial Acceleration (For a Jolt Start)
            if (_acceleration < initAccel){
                _acceleration = initAccel;
            }
            _acceleration += accelerationRate * Time.deltaTime;
        }
        else if (!_isMovementPressed && _acceleration > 0){
            _acceleration -= deccelerationRate * Time.deltaTime;
        }
        else if(_acceleration > 1) {
            _acceleration = 1;
        }
        else if (_acceleration < 0) {
            _acceleration = 0;
        }
        /** 
         *  Set the Movement Vector for X and Z
         *  Movement is the product of:
         *  - Acceleration     (multiplies by acceleration amount) [0 to 1]
         *  - Direction        (Forward or Backwards) [1 or -1]
         *  - [DIRECTION]Speed (Forward vs Backwards Speed for Walking)
         *  - Forward Vector for X and for Z (for rotational independent forward movement)
         */
        Vector3 forwardDirection = _charController.transform.forward;
        //float   directionSpeed   = 0f;
        if(_movementAxis.y > 0) {
            _directionSpeed = forwardSpeed * _movementAxis.y;
        }
        else if (_movementAxis.y < 0) {
            _directionSpeed = backwardSpeed * _movementAxis.y;
        }

        float speed = _acceleration * _directionSpeed;
        forwardDirection *= speed;
        // Set the Current Movement Vector for X and Z
        _currentMovement.x = forwardDirection.x;
        _currentMovement.z = forwardDirection.z;
    }

    /**
     ** Handle Rotate
     *  - Handle the Rotation of the Character Controller
     *!  ROTATION NEEDS WORK
     *!   - Should be a smooth transition between Rotation Speeds, as currently the
     *!   rotation is a constant per State (Idle, Walk, Run)
     */
    private void HandleRotate(){
        _appliedRotationY = RotationInput * RotationSpeed * Time.deltaTime;
        _charController.transform.Rotate(0, _appliedRotationY , 0);
    }


    /*********************************************
     ************** State Machine ****************
     *********************************************/
    protected override BaseState GetInitialState() {
        return _groundedState;
    }

    protected override void DoUpdate(){
        ////Update the SubState Variable
        //// if (RootState != null && RootState.SubState != null){
        ////     _currentLvl2State = RootState.SubState;
        //// }
        //Handle Movement and Gravity
        HandleMove();
        HandleRotate();
        HandleGravity();

        DebugMenu();

        if (transform.position.y <= 0.93f){
            SceneManager.LoadScene("GameOver");
        }

        //Mouse Pos
        _mousePos = _playerInput.CharacterControls.View.ReadValue<Vector2>();
    }

    // Print out Debug Info to the GUI
    private void DebugMenu(){
        //string content = StateList[1] != null ? "State: " + StateList[1].Name : "[NO CURRENT STATE]";
        _debugText[0].text = StateList[1] != null ? "State: " + StateList[1].Name : "[NO CURRENT STATE]";
        //content = StateList[2] != null ? "-> " + StateList[2].Name : "[NO SUB STATE]";
        _debugText[1].text = StateList[2] != null ? "-> " + StateList[2].Name : "[NO SUB STATE]";
        //content = "(" + _mousePos.x.ToString() + ", " + _mousePos.y.ToString() + ")"; 
        _debugText[2].text = "(" + _mousePos.x.ToString() + ", " + _mousePos.y.ToString() + ")"; 
        _debugText[3].text = "Forward: (" + _currentMovement.x.ToString();
        _debugText[4].text = " | " + _currentMovement.z.ToString() + ")";
        _debugText[5].text = "Acceleration: " + _acceleration;
    }
}
