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
    public bool doubleJump;
    private bool jumpPressed;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    //public bool collectedDoubleJump = false;
    //public bool collectedDash = false;
    //public bool collectedAirDash = false;

    public PlayerAudio audio;

    [SerializeField] private TrailRenderer tr;
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
            return _isMoving;
            // Return the value inside the isMoving variable just created
        } private set {
            // Set isMoving to the value is gonna be passed into the set
            _isMoving = value;
            // Set the boolean in the animator with the same value static strings
            anim.SetBool(AnimationStrings.isMoving, value);
            audio.PlayrunningAudio(_isMoving);
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

    // It's called every fixed frame-rate frame.
    private void FixedUpdate(){
    // Check if the player is in contact with the ground and is moving

    //if(touchingDirections.IsGrounded && moveInput != Vector2.zero){
        // Play the running audio clip
        //runningAudio.Play();
    //}
        //else{
            // Stop the running audio clip if the player is not in contact with the ground or not moving
            //runningAudio.Stop();
        //}

    // prevent the player to do things while dashing
        if(isDashing){
            return;
        }
        // If player is not being hit right now 
        if(!damageable.LockVelocity){
            // Move the player
            rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);
            }
            // Update the animator paramether with the current vertical velocity to update the air state machine in the animator 
            anim.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        // Check if is on the ground and is not jymping to reset the double jump bool and be able to double jump again
        if(touchingDirections.IsGrounded && !jumpPressed){
            doubleJump = false;  
        } 
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
        // If can't double jump yet 
        if(PlayerPrefs.GetInt("purchasedDoubleJump") == 0){
            // Check if the key is pressed  and if player can move and if player is on the ground or can doubleJump
            if(context.started && CanMove  && touchingDirections.IsGrounded)
            {
                //player jump audio 
                audio.PlayjumpAudio();
                // Update animator paramether using static strings  
                anim.SetTrigger(AnimationStrings.jump);
                // Add jump inpulse on the y axis 
                rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);

            }   
        } 
        // If purchased double jump or colleted
        if (PlayerPrefs.GetInt("purchasedDoubleJump") == 1 || playerInv.TemporaryDoubleJump){
            // If jump key and can move
            if(context.started && CanMove ){ 
                if(touchingDirections.IsGrounded || doubleJump){
                    // Setting this for the check in the update
                    jumpPressed = true;
                     //player jump audio 
                    audio.PlayjumpAudio();
 
                    // Update animator paramether using static strings  
                    anim.SetTrigger(AnimationStrings.jump);
                    // Add jump inpulse on the y axis 
                    rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
                    // Update double jump bool
                    doubleJump = !doubleJump;
                }
            }
            if(context.canceled){
                // Finish jump for the update 
                jumpPressed = false;
            }
        }  
    }
    
    public void OnAttack(InputAction.CallbackContext context){
        if(context.started){
            // Attack updating animator paramether
            anim.SetTrigger(AnimationStrings.attack);
             //player attack Audio
            audio.PlayattackAndHitAudio();
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context){
        // If can shoot
        if(context.started && playerInv.ThrowingSwords > 0){
            // Shoot
            anim.SetTrigger(AnimationStrings.rangedAttack);
            // Update the remaining swords
            playerInv.ThrowingSwords --;
        }
    }

    public void OnHit(int damage, Vector2 knockback){
        // Apply knockback inpulse 
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        //player damage Audio
        audio.PlaytakeDamageAudio();
    }

    public void OnDash(InputAction.CallbackContext context){
        
        if(PlayerPrefs.GetInt("purchasedDash") == 1 || playerInv.TemporaryDash){
            if(touchingDirections.IsGrounded){
                if(context.started && canDash){
                    StartCoroutine(Dash());
                }
            }
        }
        if (PlayerPrefs.GetInt("purchasedAirDash") == 1 || playerInv.TemporaryAirDash){
            if(context.started && canDash){
                StartCoroutine(Dash());
            }
        }
    }
    
    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        // Store the current gravity value
        float originalGravity = rb.gravityScale;
        // Disable gravity during the dash 
        rb.gravityScale = 0f;
        // Create the dash inpulse 
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        // Display the trail
        tr.emitting = true; 
        // Stop dashing after a certain amount of time 
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        // Give the player a cooldown to not let him abuse of the dash power
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
