using UnityEngine;

public class RunnerJumpImpulse : MonoBehaviour
{
    // Access controller
    PathfinderController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the 
        if (collision.tag == "Dervy")
        {
            if (controller.JumpEnabled == true)
            {
                // Make the pathfinder jump
                controller.Jump();
                Debug.Log("PathfinderJumpImpulse made the Pathfinder jump!");
            }
        }
    }
}