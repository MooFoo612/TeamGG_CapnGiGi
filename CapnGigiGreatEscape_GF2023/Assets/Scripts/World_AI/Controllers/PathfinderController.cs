using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathfinderController : MonoBehaviour
{

    // ---------- Inspector Access -----------|

    [Header("Pathfinding")]
    public Transform target;
    public float _aggroRange = 50f;
    public float pathUpdateFrequency = 0.5f;

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

    // -------- Path & Collisions ---------- |

    private Path path;
    private int currentWaypoint = 0;
    private RaycastHit2D check_N;
    private RaycastHit2D check_NE;
    private RaycastHit2D check_E;
    private RaycastHit2D check_SE;
    private RaycastHit2D check_S;
    private RaycastHit2D check_SW;
    private RaycastHit2D check_W;
    private RaycastHit2D check_NW;

    // ------ Accessable by Children ------- |

    private bool isGrounded;
    protected bool isBelowPlatform;
    protected Vector2 direction;
    protected Vector2 jumpDirection;
    protected Vector2 force;
    protected Collider2D pfCollider;

    // ----- Components --------- |

    Seeker seeker;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer sr;

    // -------- Local Variables ---------- |

    private Vector3 currentPos;
    private Vector3 targetPos;

    protected Vector2 Direction
    {
        get { return this.direction; } set { this.direction = value; }
    }

    protected Vector2 Force
    {
        get { return this.force; } set { this.force = value; }
    }

    protected Vector2 Position
    {
        get { return this.currentPos; } set { this.currentPos = value; }
    }

    public bool Grounded
    {
        get { return isGrounded; } set { isGrounded = value; }    
    }

    // ---------------------------------------------------------------------|


    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pfCollider = GetComponent<Collider2D>();

        // Keep on repeating the script to update the path
        InvokeRepeating("UpdatePath", 0f, pathUpdateFrequency);
    }

    private void FixedUpdate()
    {
        // If find the target and can follow, follow it through the path
        if (TargetInRange() && followEnabled)
        {
            Hunt();
        }
        // else?
    }

    private void UpdatePath()
    {
        //currentPos = transform.position;

        // If object to seek found update path 
        if (followEnabled && TargetInRange() && seeker.IsDone())
        {
            targetPos = target.position;

            seeker.StartPath(rb.position, targetPos, OnPathComplete);
        }
    }

    private void Hunt()
    {
        Vector3 startOffset;

        // If there is no path
        if (path == null)
        {
            return;
        }

        // Return out of function when end of path is reached
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // Check if colliding with anything
        startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);

        // Check if pathfinder is on the ground
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.1f);

        // Calculate direction 
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        // Calculate the force
        Vector2 force = direction * speed * Time.deltaTime;

        // If runner can jump
        if (jumpEnabled && isGrounded)
        {
            Jump();
        }
        
        // Use the force(tm) to move the runner
        rb.AddForce(force);

        // After moving, get the location of the next waypoint in the path
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        // If the distance lest than the 
        if(distance < nextWaypointDistance)
        {
            // 
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

    public void Jump()
    {
        if (rb.velocity.x > 0.01f)
        {
            // Jump following the grid dimensions
            if (direction.y > jumpNodeHeightRequirement)
            {
                sr.flipX = false;
                rb.AddForce(new Vector2(1f,1f) * speed * jumpModifier);
                animator.SetBool("isJumping", true);
            }

        }
        else if (rb.velocity.x < -0.01f)
        {
            // Jump following the grid dimensions
            if (direction.y > jumpNodeHeightRequirement)
            {
                // Flip sprite left if rb is moving left
                sr.flipX = true;
                rb.AddForce(new Vector2(-1f,-1f) * speed * jumpModifier);
                animator.SetBool("isJumping", true);
            }
        }
    }
}