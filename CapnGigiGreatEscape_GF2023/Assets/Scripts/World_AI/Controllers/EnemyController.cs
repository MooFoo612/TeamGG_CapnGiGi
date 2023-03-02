using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : PathfinderController
{/*
    private GameObject pigguTarget;
    private Path path;
    private int currentWaypoint = 0;

    new bool isGrounded;

    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pigguTarget = GameObject.Find("CapnGigi");
        // Keep on repeating the script to update the path
        InvokeRepeating("UpdatePath", 0f, pathUpdateFrequency);

    }

    private void FixedUpdate()
    {
        // If find the target and can follow, follow it through the path
        if (TargetInRange() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        // If object to seek found update path 
        if(followEnabled && TargetInRange() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        // Make sure the path isn't null and the waypoint is not already over
        if(path == null)
        {
            return;
        }
        // Reached the end of the platform
        if (currentWaypoint >= path.vectorPath.Count)
        {
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
            if(direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
                animator.SetBool("isJumping", true);
            }
        }

        // Move the character into moving direction
        rb.AddForce(force);

        // Next waypoint 
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Flip the sprite depending on the direction 
        if(directionLookEnabled)
        {
            if(rb.velocity.x > 0.01f)
            {
                sr.flipX = false;
            } 
            else if (rb.velocity.x < -0.01f)
            {
                sr.flipX = true;
            }
        }
    }
    // Return true if target is in distance to be followed 
    private bool TargetInRange()
    {
        return Vector2.Distance(transform.position, target.transform.position) < _aggroRange;
    }

    private void OnPathComplete(Path p)
    {
        // If no errors
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

    }
    */
}