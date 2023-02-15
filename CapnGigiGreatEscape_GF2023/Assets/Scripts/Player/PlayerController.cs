using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class PlayerController : MonoBehaviour{
    
    [SerializeField] private float speed = 5;
    public float airWalkSpeed = 5f;
    public float jumpImpulse = 7f;  
    private Vector2 moveInput;
    TouchingDirections touchingDirections; 
    Damageable damageable;
    public Rigidbody2D rb;
    Animator anim; 
    PlayerInventory playerInv;
    
    // CurrentSpeed function 
    public float CurrentSpeed{
        get{
            // If the player canMove(is not attacking)
            if(CanMove){
                // If is moving and is not colliding with a wall
                if(IsMoving && !touchingDirections.IsOnWall ){
                    // If is on the ground
                    if(touchingDirections.IsGrounded){       
                        // Get the player speed on ground         
                        return speed;
                    } else {
                        // If is not on the ground get the player speed on air that is a different var (we'll use it to manage the difficulty increment ofthe   game)
                        return airWalkSpeed;
                    }
                } else {
                    // Idle speed is 0
                    return 0;
                }    
            } else {
                // Movement locked 
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
            // Return the value inside the variable that is updated inside the code
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

    public bool CanMove{
        // Still the same logic as above
        get{
            return anim.GetBool(AnimationStrings.canMove);
        }
    }

    public bool isAlive{
        get{
            return anim.GetBool(AnimationStrings.isAlive);
        }
    }


    // It's called when the script is loaded (when the game start)
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        playerInv = GetComponent<PlayerInventory>();
    }
    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        
    }
    // It's called every fixed frame-rate frame.
    private void FixedUpdate(){
        // If player is not being hit right now 
        if(!damageable.LockVelocity){
            // Move the player
            rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);
        }
        // Update the animator paramether with the current vertical velocity to update the air state machine in the animator 
        anim.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }    

    // It's called while the player is moving(takes the parametheres setted on the Input System controller)
    public void OnMove(InputAction.CallbackContext context){
        // Get the player position
        moveInput = context.ReadValue<Vector2>();
        // If player is alive
        if(isAlive){
            // IsMoving setter = it pass true if the vector is actually moving and vice versa
            IsMoving = moveInput != Vector2.zero;
            // Change sprite direction
            SetFacingDirection(moveInput);
            // Check to prevent the player from kepp walking into the wall and don't fall 
            if(context.started && touchingDirections.IsOnWall){
                IsMoving = false;
            }
        // If is not alive
        } else {
            // Block movement
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
        // Check if the key is pressed and if player is on the ground and if player can move 
        if(context.started && touchingDirections.IsGrounded && CanMove ){ // 
            // update animator paramether using static strings  
            anim.SetTrigger(AnimationStrings.jump);
            // Add jump inpulse on the y axis 
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context){
        if(context.started){
            anim.SetTrigger(AnimationStrings.attack);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context){
        if(context.started && playerInv.ThrowingSwords > 0){
            anim.SetTrigger(AnimationStrings.rangedAttack);
            playerInv.ThrowingSwords --;
        }
    }

    public void OnHit(int damage, Vector2 knockback){
        // Apply knockback inpulse 
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
