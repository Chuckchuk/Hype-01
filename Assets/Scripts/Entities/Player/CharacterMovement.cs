using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    //Public Variables
    public float walk_speed_forward = 1f;
    public float walk_speed_backward = 0.6f;
    public float force_gravity = 1f;

    public float rotation_per_frame;
    
    //Type Variables
    PlayerInput playerInput;
    CharacterController controller;

    // Storage Variables
    Vector3 CurrentMovement;


    bool MovingForward;
    bool MovingBackward;



    void Awake() {
        playerInput = new PlayerInput();
        controller = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.performed += context => { MoveForward(context.ReadValue<float>()); };
        playerInput.CharacterControls.Move.canceled += context => { MoveForward(context.ReadValue<float>()); };

    }

    void OnEnable() {
        // Enable Character Controls Action Map
        playerInput.CharacterControls.Enable();
    }

    void OnDisable() {
        // Disable Character Controls Action Map
        playerInput.CharacterControls.Disable();
    }


    // Takes in the X and Z input and translates it to Movement
    void MoveForward(float direction) {
        //if(controller.isGrounded){
            CurrentMovement.z = direction;
        //}
        
        //Set Booleans for Forward or Backward Motion
        if (direction > 0){
            MovingForward = true;
            MovingBackward = false;
        }
        else if (direction < 0){
            MovingForward = false;
            MovingBackward = true;
        }
        else{
            MovingForward = false;
            MovingBackward = false;
        }
    }

    /**
     * Handle Movement
     *  Forward      - At 1x   Speed  - On "Forward" (W Default) Press
     *  Backward     - At 0.6x Speed  - On "Backward (S Default) Press
     *  Rotate Left  - At 1x   Speed  - On "Left"    (A Default) Press
     *  Rotate Right - At 1x   Speed  - On "Right"   (D Default) Press
     */
    void handleMovement() {
        if (MovingForward){
            controller.Move(CurrentMovement * Time.deltaTime * walk_speed_forward);
        }
        else if (MovingBackward){
            controller.Move(CurrentMovement * Time.deltaTime * walk_speed_backward);
        }
    }
    void handleRotation() {
        //Quaternion currentRotation;

        


    }


    /**
     * Handle Gravity
     *  Apply Gravity to the character
     */
    void handleGravity() { 
        float gravity = 0f;
        if (controller.isGrounded) { 
            gravity = -0.05f;
        }
        else {
            gravity = -force_gravity;
        }
        CurrentMovement.y = gravity;

    }

    // Update is called once per frame
    void Update() {
        handleMovement();
        handleRotation();

        //Keep as Last
        handleGravity();
    }

 
}
  