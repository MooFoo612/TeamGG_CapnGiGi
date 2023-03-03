using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collisions), typeof(Damageable))]

public class Krabby_Test : MonoBehaviour{
    public float walkSpeed1 = 3f;
    public DetectionZone attackZone1;
    public float walkStopRate1 = 0.05f;
    Rigidbody2D rb1;
    Animator anim1;
    Collisions touchingDirections1;
    Damageable damageable1;
    public enum WalkableDirection {Right, Left}
    private WalkableDirection _walkDirection1;
    private Vector2 walkDirectionVector1 = Vector2.right;  
    public WalkableDirection WalkDirection1{
        get{
            // The get works with the same logic of the player 
            return _walkDirection1;
        } set {
            // If the value does't correspond 
            if(_walkDirection1 != value){
                // Flip sprite direction using localScale 
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                // If direction is right
                if(value == WalkableDirection.Right){
                    // Move right
                    walkDirectionVector1 = Vector2.right;
                } else if (value == WalkableDirection.Left){
                    // Move left
                    walkDirectionVector1 = Vector2.left;
                }
            }
            // Set the value with the one got in the get 
            _walkDirection1 = value;
        }
    }

    public bool _hasTarget1 = false;
    public bool HasTarget1{
        get{
            // Get the current value
            return _hasTarget1;
        } private set {
            // Set it with the updated value
            _hasTarget1 = value;
            // Update paramether into the animator
            anim1.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove1{
        get{
            return anim1.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake(){
        rb1 =  GetComponent<Rigidbody2D>();
        touchingDirections1 = GetComponent<Collisions>();
        anim1 = GetComponent<Animator>();
        damageable1 = GetComponent<Damageable>();
    }

    private void Update(){
        // look in the other script list for a target
        HasTarget1 = attackZone1.detectedColliders.Count > 0;
    }

    private void FixedUpdate(){
        // If enemy is colliding with a wall while he is walking on ground
        if(touchingDirections1.IsGrounded && touchingDirections1.IsOnWall){
            // Flip direction
            FlipDirection();
        }
        // If not just being hit 
        if(!damageable1.LockVelocity){
            // If canMove is true (enemy is not attacking)
            if(CanMove1){
                // Move the enemy
                rb1.velocity = new Vector2(walkSpeed1 * walkDirectionVector1.x, rb1.velocity.y);
            } else {
                // Enda you'll love this one, I'm using interpolation xD 
                // Making this so that the enemy slides a bit before to stop and perform the attack
                // Each time it goes through walkStopRate it's going to move it towards of zero at a certain percentage between 0 and 1 (1 being 100%) on each call for the MathFunction for the interpolation ,so  in this case it's like per each fixed frame. The function actually returns the interpolated float    result between the two float values.
                // Be proud of me pls 
                rb1.velocity = new Vector2(Mathf.Lerp(rb1.velocity.x, 0, walkStopRate1), rb1.velocity.y);
            }
        }

        
    }
    
    private void FlipDirection(){
        // If Direction is right 
        if(WalkDirection1 == WalkableDirection.Right){
            // Flip the vector (So the direction)
            WalkDirection1 = WalkableDirection.Left;
            Debug.Log("krabby is going " + WalkDirection1);
        // Else if direction is left
        } else if (WalkDirection1 == WalkableDirection.Left){
            // Go right 
            WalkDirection1 = WalkableDirection.Right;
            Debug.Log("krabby is going " + WalkDirection1);
        // Check for bugs 
        } else {
            Debug.LogError("Current Walkable direction not setted to a legal value (left or right) ");
        }
    }

    public void OnHit(int damage, Vector2 knockback){
        // Apply knockback inpulse 
        rb1.velocity = new Vector2(knockback.x, rb1.velocity.y + knockback.y);
    }
}
