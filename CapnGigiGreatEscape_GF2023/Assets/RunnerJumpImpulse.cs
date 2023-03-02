using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RunnerJumpImpulse : MonoBehaviour
{
    PathfinderController controller;
    bool isGrounded;
    bool jumpEnabled;
    private void Start()
    {
    }

    private void FixedUpdate()
    {
        // Check if the pathfinder is on the ground
        isGrounded = controller.Grounded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pathfinder_Jump")
        {
            // Make the pathfinder jump
            controller.Jump();
        }
    }
}