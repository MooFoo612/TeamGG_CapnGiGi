using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour{

[SerializeField] private float speed = 1;
private Rigidbody2D rb;
private Vector2 movement;
    // Start is called before the first frame update
    void Start(){
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update(){
        
    }

    private void FixedUpdate(){
        // Move the player using Unity Input System
        rb.MovePosition(rb.position + (movement * speed * Time.fixedDeltaTime));
    }

    // It's called while the player is moving 
    void OnMove(InputValue movePosition){
        // Get the player position
        movement= movePosition.Get<Vector2>();
    }
}
