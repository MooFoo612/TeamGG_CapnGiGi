using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour{

[SerializeField] private float speed = 5;
private Vector2 moveInput;
[SerializeField]private bool _isMoving = false;

public bool IsMoving { 
    get{
        // Return the value inside the isMoving variable just created
        return _isMoving;
    } private set {
        // Set isMoving to the value is gonna be passed into the set
        _isMoving = value;
        // Set the boolean in the animator with the same value
        animator.SetBool("isMoving", value);
    }
}
public Rigidbody2D rb;
Animator anim;



public float jumpImpulse = 10f;

    // It's called when the script is loaded (when the game start)
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);

    }    

    // It's called while the player is moving(takes the parametheres setted on the Input System controller)
    public void OnMove(InputAction.CallbackContext context){
        // Get the player position
        moveInput = context.ReadValue<Vector2>();
        // IsMoving setter = it pass true if the vector is actually moving and vice versa
        IsMoving = moveInput != Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context){
        // Check if the key is pressed
        if(context.started){
            // Add jump inpulse on the x axis 
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
}
