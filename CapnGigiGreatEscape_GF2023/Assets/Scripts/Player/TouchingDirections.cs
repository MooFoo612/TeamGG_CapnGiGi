using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour{

    // Uses the collider to check directions to see if the object is currently on the ground, touching the wall, or touching the ceiling 

    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    //CapsuleCollider2D = touchingCol;
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

    private void Awake(){
        //touchingCol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate(){
        // This function will store the result in the groundHits array and will return the number of collision that this cast detected as an int 
        //IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }

}
