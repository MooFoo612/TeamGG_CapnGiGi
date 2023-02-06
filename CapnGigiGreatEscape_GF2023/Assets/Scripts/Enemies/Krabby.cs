using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class Krabby : MonoBehaviour{
    public float walkSpeed = 3f;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    public enum WalkableDirection {Right, Left}
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.left;  
    public WalkableDirection WalkDirection{
        get{
            // The get works with the same logic of the player 
            return _walkDirection;
        } set {
            // If the value does't correspond 
            if(_walkDirection != value){
                // Flip direction
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

    private void Awake(){
        rb =  GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    private void FixedUpdate(){
        // If enemy is colliding with a wall while he is walking on ground
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall){
            // Flip direction
            FlipDirection();
        }
        // Move the enemy
        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
    }
    
    private void FlipDirection(){
        // If Direction is right 
        if(WalkDirection == WalkableDirection.Right){
            // Flip the vector (So the direction)
            WalkDirection = WalkableDirection.Left;
        // Else if direction is left
        } else if (WalkDirection ==  WalkableDirection.Left){
            // Go right 
            WalkDirection = WalkableDirection.Right;
        // Check for bugs 
        } else {
            Debug.LogError("Current Walkable direction not setted to a legal value (left or right) ");
        }
    }
}
