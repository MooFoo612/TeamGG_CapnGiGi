using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RunnerAI : MonoBehaviour
{
    [Header("Pathfinding")]
    private Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled =  true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded;
    bool isMovingX;
    bool isJumping;
    bool inTheAir;
    bool isLanding;
    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;

    public void Start(){
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Keep on repeating the script to update the path
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        target = GameObject.Find("CapnGigi").transform;
    }

    private void FixedUpdate(){
        // If find the target and can follow, follow it through the path
        if (TargetInDistance() && followEnabled){
            PathFollow();
        }
    }

    private void UpdatePath(){
        // If object to seek found update path 
        if(followEnabled && TargetInDistance() && seeker.IsDone()){
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow(){
        // Make sure the path isn't null and the waypoint is not already over
        if(path == null){
            return;
        }
        // Reached the end of the platform
        if (currentWaypoint >= path.vectorPath.Count){
            return;
        }
        // Check if colliding with anythinhg
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        // Calculate direction
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        // If can jump
        if (jumpEnabled && isGrounded){
            // Jump following the grid dimensions
            if(direction.y > jumpNodeHeightRequirement){
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }
        // Move the character into moving direction
        rb.AddForce(force);
        // Next waypoint 
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance){
            currentWaypoint++;
        }
        // Flip the sprite depending on the direction 
        if(directionLookEnabled){
            if(rb.velocity.x > 0.05f){
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else if (rb.velocity.x < -0.05f){
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
    // Return true if target is in distance to be followed 
    private bool TargetInDistance(){
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p){
        // If no errors
        if (!p.error){
            path = p;
            currentWaypoint = 0;
        }

    }
    

}