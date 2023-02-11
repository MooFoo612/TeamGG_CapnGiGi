using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour{

    // Uses the collider to check directions to see if the object is currently on the ground, touching the wall, or touching the ceiling 
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    CapsuleCollider2D touchingCol;
    Animator anim; 
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    [SerializeField] private bool _isGrounded;
    // IsGrounded function
    public bool IsGrounded {
        get{
            // Return the value inside the _isGrounded variable just created
            return _isGrounded;
        } private set {
            // Set _isGrounded to the value is gonna be passed into the set
            _isGrounded = value;
            // Set the boolean in the animator with the same value using static strings
            anim.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    // If player is watching at his right set the vector 2 to the right direction, otherwise set it to the left direction
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    public float wallDistance = 0.2f;
    [SerializeField] private bool _isOnWall;
    // IsOnWall function
    public bool IsOnWall {
        get{
            // Return the value inside the _isOnWall variable just created
            return _isOnWall;
        } private set {
            // Set _isOnWall to the value is gonna be passed into the set
            _isOnWall = value;
            // Set the boolean in the animator with the same value using static strings
            anim.SetBool(AnimationStrings.isOnWall, value);
        }
    }
    
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    public float ceilingDistance = 0.05f;
    [SerializeField] private bool _isOnCeiling;
    // IsOnCeiling function
    public bool IsOnCeiling {
        get{
            // Return the value inside the _isOnCeiling variable just created
            return _isOnCeiling;
        } private set {
            // Set _isOnCeiling to the value is gonna be passed into the set
            _isOnCeiling = value;
            // Set the boolean in the animator with the same value using static strings
            anim.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake(){
        touchingCol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate(){
        // Wall, Ground and ceiling cheks 
        // This function will store the result in the groundHits array and will return the number of collision that this cast detected as an int 
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        // Similiar logic for the walls check
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        // Same logic as ground for the ceiling
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }

}
