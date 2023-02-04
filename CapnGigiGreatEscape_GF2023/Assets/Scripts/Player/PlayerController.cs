using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour{
    
    [SerializeField] private float speed = 5;
    public float airWalkSpeed = 5f;
    public float jumpImpulse = 7f;  
    private Vector2 moveInput;
    TouchingDirections touchingDirections;
    public Rigidbody2D rb;
    Animator anim;
    
    public float CurrentSpeed{
        get{
            if(IsMoving && !touchingDirections.IsOnWall ){
                if(touchingDirections.IsGrounded){                   
                    return speed;
                } else {
                return airWalkSpeed;
                }
            // Air state checks
        } else {
            return 0;
        }    
        }
    }
    [SerializeField]private bool _isMoving = false;
    // IsMoving function 
    public bool IsMoving { 
        get{
            // Return the value inside the isMoving variable just created
            return _isMoving;
        } private set {
            // Set isMoving to the value is gonna be passed into the set
            _isMoving = value;
            // Set the boolean in the animator with the same value static strings
            anim.SetBool(AnimationStrings.isMoving, value);
        }
    }
    [SerializeField]private bool _isFacingRight = true;
    // IsFacingRight function
    public bool IsFacingRight{
        get{
            // Return the value inside the  variable that is updated inside the code
            return _isFacingRight;
        } private set {
            // If get false as a paramether
            if(_isFacingRight != value){
                // Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            // Set the variable with the value passet in the set 
            _isFacingRight = value;
        }
    }

    // It's called when the script is loaded (when the game start)
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        
    }
    // It's called every fixed frame-rate frame.
    private void FixedUpdate(){
        // Move the player using Unity Input System
        rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);
        // Update the animator paramether with the current vertical velocity to update the air state machine in the animator 
        anim.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }    

    // It's called while the player is moving(takes the parametheres setted on the Input System controller)
    public void OnMove(InputAction.CallbackContext context){
        // Get the player position
        moveInput = context.ReadValue<Vector2>();
        // IsMoving setter = it pass true if the vector is actually moving and vice versa
        IsMoving = moveInput != Vector2.zero;
        // Change sprite direction
        SetFacingDirection(moveInput);
        if(context.started && touchingDirections.IsOnWall){
            IsMoving = false;
        }

    }

    private void SetFacingDirection(Vector2 moveInput){
        // If the player is moving right and is not facing right
        if(moveInput.x > 0 && !IsFacingRight){
            // Face the right
            IsFacingRight = true;

        // If the player is moving left and is facing right    
        } else if(moveInput.x < 0 && IsFacingRight){
            // Face the left
            IsFacingRight = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context){
        // Check if the key is pressed and if player is on the ground
        if(context.started && touchingDirections.IsGrounded ){ // 
            // update animator paramether using static strings  
            anim.SetTrigger(AnimationStrings.jump);
            // Add jump inpulse on the y axis 
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
}
