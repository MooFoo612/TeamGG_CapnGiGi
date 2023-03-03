using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RunnerJumpImpulse : PathfinderController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the 
        if (collision.tag == "Pathfinder_Jump" && Direction.y > jumpNodeHeightRequirement)
        {
            if (jumpEnabled && IsGrounded == true)
            {
                // Make the pathfinder jump
                Jump();
                Debug.Log("PathfinderJumpImpulse made the Pathfinder jump!");
            }
        }
    }
}