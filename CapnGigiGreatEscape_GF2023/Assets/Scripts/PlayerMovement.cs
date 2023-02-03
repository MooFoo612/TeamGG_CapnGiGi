using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour{

[SerializeField] private float speed = 5;
public bool IsMoving { get; private set;}
private Rigidbody2D rb;
private Vector2 moveInput;

    // It's called when the script is loaded (when the game start)
    private void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
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

    // It's called while the player is moving 
    public void OnMove(InputAction.CallbackContext context){
        // Get the player position
        moveInput = context.ReadValue<Vector2>();
        // Set isMoving to true when player is moving
        IsMoving = moveInput != Vector2.zero;
    }
}
