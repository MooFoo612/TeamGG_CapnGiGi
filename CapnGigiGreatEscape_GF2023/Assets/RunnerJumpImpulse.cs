using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RunnerJumpImpulse : MonoBehaviour
{
    PathfinderController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the 
        if (collision.tag == "Pathfinder_Jump" && controller.Direction.y > controller.jumpNodeHeightRequirement)
        {
            if (controller.jumpEnabled && controller.Grounded == true)
            {
                // Make the pathfinder jump
                controller.Jump();
                Debug.Log("PathfinderJumpImpulse made the Pathfinder jump!");
            }
        }
    }
}