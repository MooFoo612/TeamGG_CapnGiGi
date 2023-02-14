using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class Star : MonoBehaviour{
    public float maxSpeed = 3f;
    public float walkAcceleration = 3f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public float walkStopRate = 0.05f;
    Rigidbody2D rb;
    Animator anim;
    TouchingDirections touchingDirections;
    Damageable damageable;
    public enum WalkableDirection {Right, Left}
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;  
    public WalkableDirection WalkDirection{
        get{
            // The get works with the same logic of the player 
            return _walkDirection;
        } set {
            // If the value does't correspond 
            if(_walkDirection != value){
                // Flip sprite direction using localScale 
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                // If direction is right
                if(value == WalkableDirection.Right){
                    // Move right
                    walkDirectionVector = Vector2.right;
                } else if (value == WalkableDirection.Left){
                    // Move left
                    walkDirectionVector = Vector2.left;
                }
            }
            // Set the value with the one got in the get 
            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget{
        get{
            // Get the current value
            return _hasTarget;
        } private set {
            // Set it with the updated value
            _hasTarget = value;
            // Update paramether into the animator
            anim.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove{
        get{
            return anim.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown{
        get{
            return anim.GetFloat(AnimationStrings.attackCooldown);
        } private set {
            // The mathf.max is there to be sure that the value doesn't go under 0
            anim.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Awake(){
        rb =  GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Update(){
        // look in the other script list for a target
        HasTarget = attackZone.detectedColliders.Count > 0;
        // Update the timer by reducing it if it is going on 
        if(AttackCooldown > 0){
            AttackCooldown -= Time.deltaTime;
        }
    }
        

    private void FixedUpdate(){
        // If enemy is colliding with a wall while he is walking on ground
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall){
            // Flip direction
            FlipDirection();
        }
        // If not just being hit 
        if(!damageable.LockVelocity){
            // If canMove is true (enemy is not attacking) and is not in the air
            if(CanMove &&  touchingDirections.IsGrounded){
                // Accelerate towards max speed and move the enemy
                //(Start with the velocity, than increase it in the right direction and limit it with the clamp to the two limits of the maxSpeed var)
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
            } else {
                // Enda you'll love this one, I'm using interpolation xD 
                // Making this so that the enemy slides a bit before to stop and perform the attack
                // Each time it goes through walkStopRate it's going to move it towards of zero at a certain percentage between 0 and 1 (1 being 100%) on each call for the MathFunction for the interpolation ,so  in this case it's like per each fixed frame. The function actually returns the interpolated float    result between the two float values.
                // Be proud of me pls 
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }
    
    private void FlipDirection(){
        // If Direction is right 
        if(WalkDirection == WalkableDirection.Right){
            // Flip the vector (So the direction)
            WalkDirection = WalkableDirection.Left;
            Debug.Log("krabby is going " + WalkDirection);
        // Else if direction is left
        } else if (WalkDirection == WalkableDirection.Left){
            // Go right 
            WalkDirection = WalkableDirection.Right;
            Debug.Log("krabby is going " + WalkDirection);
        // Check for bugs 
        } else {
            Debug.LogError("Current Walkable direction not setted to a legal value (left or right) ");
        }
    }

    public void OnHit(int damage, Vector2 knockback){
        // Apply knockback inpulse 
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    // Flip direction if cliff detected
    public void OnCliffDetected(){
        if(touchingDirections.IsGrounded){
            FlipDirection();
        }
    }
}
