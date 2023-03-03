using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
[RequireComponent(typeof(Rigidbody2D), typeof(Collisions), typeof(Damageable))]

public class TestPlayerController : MonoBehaviour
{
    #region Inspector Access

    [Header("Movement")]
    [SerializeField] Vector2 _moveInput;
    [SerializeField] float _groundSpeed = 5;
    [SerializeField] bool _canDash = true;
    [SerializeField] bool _isDashing;
    [SerializeField] float _dashingPower = 24;
    [SerializeField] float _dashTime = 0.2f;
    [SerializeField] float _dashCD = 1f;

    [Header("Jumping")]
    [SerializeField] float _airVelocity = 5f;
    [SerializeField] float _jumpForce = 7f;
    [SerializeField] bool _doubleJump;
    [SerializeField] bool _jumpPressed;

    [Header("Components")]
    [SerializeField] TrailRenderer _trail;
    [SerializeField] Rigidbody2D _player;
    [SerializeField] Animator _anim;
    [SerializeField] Animator _animPU;

    [Header("Script Access")]
    [SerializeField] PlayerInventory playerInv;
    [SerializeField] AudioSource runningAudio;
    [SerializeField] Collisions touchingDirections;
    [SerializeField] Damageable damageable;
    [SerializeField] new PlayerAudio audio;

    #endregion

    #region Properties

    public float CurrentSpeed
    {
        get
        {
            // If the player canMove(is not attacking)
            if(CanMove)
            {
                // If is moving and is not colliding with a wall
                if(IsMoving && !touchingDirections.IsOnWall )
                {
                    // If is on the ground
                    if(touchingDirections.IsGrounded)
                    {       
                        // Get the player speed on ground         
                        return _groundSpeed;
                    } 
                    else 
                    {
                        // If is not on the ground get the player speed on air that is a different var (we'll use it to manage the difficulty increment ofthe   game)
                        return _airVelocity;
                    }
                } 
                else 
                {
                    // Idle speed is 0
                    return 0;
                }    
            } 
            else 
            {
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
            _anim.SetBool(AnimationStrings.isMoving, value);

            //dustParticles.Play();
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
            return _anim.GetBool(AnimationStrings.canMove);
        }
    }

    public bool isAlive{
        get{
            return _anim.GetBool(AnimationStrings.isAlive);
        }
    }

    #endregion


    // It's called when the script is loaded (when the game start)
    private void Awake(){
        _player = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _animPU = GetComponent<Animator>();
        touchingDirections = GetComponent<Collisions>();
        damageable = GetComponent<Damageable>();
        playerInv = GetComponent<PlayerInventory>();
        runningAudio = GetComponent<AudioSource>();
        //particleAnim = GetComponentInChildren<ParticleAnimations>();
    }

    // It's called every fixed frame-rate frame.
    private void FixedUpdate(){
    // Prevent the player to do things while dashing
        if(_isDashing){
            return;
        }
        // If player is not being hit right now 
        if(!damageable.LockVelocity){
            // Move the player
            _player.velocity = new Vector2(_moveInput.x * CurrentSpeed, _player.velocity.y);
        }

        /* I'm wrecked now, Fab, but here I'm trying to implement one of the things
         * from the video I just put in code resources. I haven't the head to figure out what 
         * the best checks to run here are, so far I've tried these if I remember correctly: 
         * 
         * It would also be nice to try and add coyote time. I don't mind working with you on that
         *      
                if (!touchingDirections.IsGrounded && rb.velocity.y > 0)
                if (!touchingDirections.IsGrounded && jumpPressed == false)
                if (!touchingDirections.IsGrounded && jumpImpulse > 0)
        if (!touchingDirections.IsGrounded && rb.velocity.y > 0f && jumpPressed == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.8f);
        }
        
        
         */
        
        
            // Update the animator paramether with the current vertical velocity to update the air state machine in the animator 
            _anim.SetFloat(AnimationStrings.yVelocity, _player.velocity.y);

        // Check if is on the ground and is not jymping to reset the double jump bool and be able to double jump again
        if(touchingDirections.IsGrounded && !_jumpPressed){
            _doubleJump = false;  
        } 

        if (IsMoving && touchingDirections.IsGrounded)
        {
            //particleAnim.anim.SetBool("isMoving", true);

            if (!runningAudio.isPlaying)
            {
                runningAudio.Play();
            }            
        } 
        else 
        {
            runningAudio.Stop();
        }
    }    

    // It's called while the player is moving(takes the parametheres setted on the Input System controller)
    public void OnMove(InputAction.CallbackContext context){
        // Get the player position
        _moveInput = context.ReadValue<Vector2>();
        // If player is alive
        if(isAlive){
            // IsMoving setter = it pass true if the vector is actually moving and vice versa
            IsMoving = _moveInput != Vector2.zero;
            // Change sprite direction
            SetFacingDirection(_moveInput);
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

    public void OnJump(InputAction.CallbackContext context){        // Add interactions in OnJump?
        // If can't double jump yet 
        if(PlayerPrefs.GetInt("purchasedDoubleJump") == 0){
            // Check if the key is pressed  and if player can move and if player is on the ground or can doubleJump
            if(context.started && CanMove && touchingDirections.IsGrounded)
            {
                //player jump audio 
                audio.PlayjumpAudio();
                // Dust Particles
                //particleAnim.anim.SetBool("isJumping", true);
                // Update animator paramether using static strings  
                _anim.SetTrigger(AnimationStrings.jump);
                // Add jump inpulse on the y axis 
                _player.velocity = new Vector2(_player.velocity.x, _jumpForce);

            }  
        } 
        // If purchased double jump or colleted
        if (PlayerPrefs.GetInt("purchasedDoubleJump") == 1 || playerInv.TemporaryDoubleJump){
            // If jump key and can move
            if(context.started && CanMove ){ 
                if(touchingDirections.IsGrounded || _doubleJump){
                    // Setting this for the check in the update
                    _jumpPressed = true;
                     //player jump audio 
                    audio.PlayjumpAudio();
                    // Update animator paramether using static strings  
                    _anim.SetTrigger(AnimationStrings.jump);
                    // Add jump inpulse on the y axis 
                    _player.velocity = new Vector2(_player.velocity.x, _jumpForce);
                    // Update double jump bool
                    _doubleJump = !_doubleJump;
                }
            }
            if (context.canceled){
                // Finish jump for the update 
                _jumpPressed = false;
            }
        }  
    }
    
    public void OnAttack(InputAction.CallbackContext context){
        if(context.started){
            // Attack updating animator paramether
            _anim.SetTrigger(AnimationStrings.attack);
            if(PlayerPrefs.GetInt("swordAttackPowerUp") == 1){
                Debug.Log("POwer up prefs setted to 1");
                _animPU.SetTrigger(AnimationStrings.attack);
            }
             //player attack Audio
            audio.PlayattackAndHitAudio();
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context){
        // If can shoot
        if(context.started && playerInv.ThrowingSwords > 0){
            // Shoot
            _anim.SetTrigger(AnimationStrings.rangedAttack);
            // Update the remaining swords
            playerInv.ThrowingSwords --;
        }
    }
    public void OnHit(int damage, Vector2 knockback){
        // Apply knockback inpulse 
        _player.velocity = new Vector2(knockback.x, _player.velocity.y + knockback.y);
        //player damage Audio
        audio.PlaytakeDamageAudio();
    }

    public void OnDash(InputAction.CallbackContext context){
        
        if(PlayerPrefs.GetInt("purchasedDash") == 1 || playerInv.TemporaryDash){
            if(touchingDirections.IsGrounded){
                if(context.started && _canDash){
                    StartCoroutine(Dash());
                }
            }
        }
        if (PlayerPrefs.GetInt("purchasedAirDash") == 1 || playerInv.TemporaryAirDash){
            if(context.started && _canDash){
                StartCoroutine(Dash());
            }
        }
    }
    
    private IEnumerator Dash(){
        _canDash = false;
        _isDashing = true;
        // Store the current gravity value
        float originalGravity = _player.gravityScale;
        // Disable gravity during the dash 
        _player.gravityScale = 0f;
        // Create the dash inpulse 
        _player.velocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
        // Display the trail
        _trail.emitting = true; 
        // Stop dashing after a certain amount of time 
        yield return new WaitForSeconds(_dashTime);
        _trail.emitting = false;
        _player.gravityScale = originalGravity;
        _isDashing = false;
        // Give the player a cooldown to not let him abuse of the dash power
        yield return new WaitForSeconds(_dashCD);
        _canDash = true;
    }
}
