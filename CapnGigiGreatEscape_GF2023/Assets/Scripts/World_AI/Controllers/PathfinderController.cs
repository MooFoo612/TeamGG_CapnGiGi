using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
//using System.IO;
using System;
using Unity.VisualScripting;

public class PathfinderController : MonoBehaviour
{
    // ---------- Inspector Access -----------|

    [Header("Pathfinding")]
    [SerializeField] Transform target;
    [SerializeField] bool followEnabled = true;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 currentPos;
    [SerializeField] float _aggroRange = 150f;
    [SerializeField] float pathUpdateFrequency = 0.1f;

    [Header("Movement")]
    [SerializeField] Vector2 movement;
    [SerializeField] private Vector2 direction;
    [SerializeField] Vector2 lastPosition;
    [SerializeField] float groundSpeed = 5f;
    [SerializeField] float maxSpeed = 25f;
    [SerializeField] float acceleration = 5f;
    [SerializeField] float nextWaypointDistance = 1.5f;

    [Header("Jumping")]
    [SerializeField] bool jumpEnabled = true;
    [SerializeField] bool jumpBuffer;
    [SerializeField] float jumpTimer = 1f;
    [SerializeField] protected float jumpNodeHeightRequirement = 1f;
    [SerializeField] float jumpModifier = 0.5f;
    [SerializeField] float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]    
    public bool directionLookEnabled = true;

    // -------- Path & Collisions ---------- |
    [SerializeField] private float _isMovingY;

    private Path path;
    private int currentWaypoint = 0;
    public Vector2 lastVelocity;
    public Vector2 lastPos;
    public bool lastGrounded;

    private RaycastHit2D check_N;
    private RaycastHit2D check_NE;
    private RaycastHit2D check_E;
    private RaycastHit2D check_SE;
    private RaycastHit2D check_S;
    private RaycastHit2D check_SW;
    private RaycastHit2D check_W;
    private RaycastHit2D check_NW;


    private GameObject cam;
    private bool offCamera;
    private Vector2 force;
    private bool isGrounded;
    protected Collider2D pfCollider;

    // ----- Components --------- |

    Seeker seeker;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected SpriteRenderer sr;

    // -------- Local Variables ---------- |


    public Vector2 Direction
    {
        get { return this.direction; }
        set { this.direction = value; }
    }

    public Vector2 Force
    {
        get { return this.force; }
        set { this.force = value; }
    }

    public Vector3 Position
    {
        get { return this.currentPos; }
        set { this.currentPos = value; }
    }

    


    public Vector3 LastPosition => this.lastPos;

    Predicate<bool> jumpLimited;




// ---------------------------------------------------------------------|



[SerializeField] private bool _isMovingX;
    // IsMoving function 
    public bool IsMovingX
    {
        get
        {
            // Returns the value of _isMoving after check
            return _isMovingX;
        }
        private set
        {
            // Set the value
            value = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

            // Set the boolean in the animator 
            anim.SetBool(AnimationStrings.isMoving, value);
        }
    }

    // IsMoving function 
    public float IsMovingY
    {
        get
        {
            // Returns the value of _isMoving after check
            return _isMovingY;
        }
        private set
        {
            value = rb.velocity.y;

            // Set the boolean in the animator 
            anim.SetFloat(AnimationStrings.yVelocity, value);
        }
    }

    [SerializeField] private bool _isGrounded;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
        }
    }


    [SerializeField] private bool _isFacingRight = true;
    // IsFacingRight function
    public bool IsFacingRight
    {
        get
        {
            // Return the value inside the variable that is updated inside the code
            return _isFacingRight;
        }
        private set
        {
            // If get false as a paramether
            if (_isFacingRight != value)
            {
                // Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            // Set the variable with the value passet in the set 
            _isFacingRight = value;
        }
    }






    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pfCollider = GetComponent<Collider2D>();
        cam = GameObject.FindWithTag("CinemachineCam");

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
    }

    private void LateUpdate()
    {
        // For per-frame comparisons
        lastPos = transform.position;
        lastGrounded = isGrounded;
        lastVelocity = new Vector2 (Mathf.Abs(rb.velocity.x), Mathf.Abs(rb.velocity.y));
    }

    private void UpdatePath()
    {
        // If object to seek found update path 
        if (followEnabled && TargetInRange() && seeker.IsDone())
        {
            targetPos = target.position;

            seeker.StartPath(rb.position, targetPos, OnPathComplete);
        }
    }

    private void Hunt()
    {
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
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);









        // Check if pathfinder is on the ground
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.1f);

        // Calculate direction 
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        // Calculate the force
        Vector2 force = direction * groundSpeed * Time.deltaTime;


        // Jump following the grid dimensions
        if (direction.y > jumpNodeHeightRequirement && target.position.y > (Position.y + 1))
        {
            jumpEnabled = true;

            if (jumpEnabled && isGrounded)
            {
                Jump();
            }

        }

        // Use the force(tm) to move the runner
        rb.AddForce(force);

        // After moving, get the location of the next waypoint in the path
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        // If the distance lest than the 
        if (distance < nextWaypointDistance)
        {
            // 
            currentWaypoint++;
        }

        // Flip the sprite depending on the direction 
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.01f)
            {
                transform.localScale = new Vector2();
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
            SetFacing();
            rb.AddForce(new Vector2(3, 3) * groundSpeed * jumpModifier);
            anim.SetBool("isJumping", true);

        }
        else if (rb.velocity.x < -0.01f)
        {
            // Flip sprite left if rb is moving left
            SetFacing();
            rb.AddForce(new Vector2(3, 3) * groundSpeed * jumpModifier);
            anim.SetBool("isJumping", true);
        }
    }


    private void SetFacing()
    {
        // If the player is moving right and is not facing right
        if (movement.x > 0 && !IsFacingRight)
        {
            // Face the right
            IsFacingRight = true;

            // If the player is moving left and is facing right    
        }
        else if (movement.x < 0 && IsFacingRight)
        {
            // Face the left
            IsFacingRight = false;
        }
    }

    private void OffCameraCheck()
    {
        // camera.Find
    }

    private IEnumerator JumpLimiter()
    {
        yield return new WaitForSeconds(0.2f);

        jumpBuffer = true;
    }

    private IEnumerator MovementValues()
    {
        yield return new WaitForSeconds(0.01f);

        movement = rb.velocity;
        SetFacing();

        
    }
}