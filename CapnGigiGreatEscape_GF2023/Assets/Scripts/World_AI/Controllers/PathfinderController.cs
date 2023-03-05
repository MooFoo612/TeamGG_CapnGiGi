using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
//using System.IO;
using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem.XR;

public class PathfinderController : MonoBehaviour
{
    // ---------- Inspector Access -----------|

    [Header("Pathfinding")]
    [SerializeField] Transform _target;
    [SerializeField] bool _followEnabled = true;
    [SerializeField] Vector3 _targetPos;
    [SerializeField] Vector3 _currentPos;
    [SerializeField] int _currentWaypoint = 0;
    [SerializeField] float _nextWaypointDistance = 1.5f;
    [SerializeField] protected float _jumpNodeHeightRequirement = 1f;
    [SerializeField] float _aggroRange = 150f;
    [SerializeField] float _pathUpdateFrequency = 0.1f;

    [Header("Movement")]
    [SerializeField] Vector2 _movement;
    [SerializeField] bool _isMovingX;
    [SerializeField] float _isMovingY;
    [SerializeField] bool _isGrounded;
    [SerializeField] bool _lastGrounded;
    [SerializeField] bool _isFacingRight;
    [SerializeField] protected Vector2 _direction;
    [SerializeField] Vector2 _lastPosition;
    [SerializeField] Vector2 _lastVelocity;
    [SerializeField] float _groundSpeed = 5f;
    [SerializeField] float _maxSpeed = 25f;
    [SerializeField] float _acceleration = 5f;
    [SerializeField] Vector2 _force;

    [Header("Jumping")]
    [SerializeField] protected bool _jumpEnabled = true;
    [SerializeField] bool jumpBuffer;
    [SerializeField] float _jumpTimer = 1f;
    [SerializeField] float jumpModifier = 0.5f;
    [SerializeField] float jumpCheckOffset = 0.1f;

    [Header("Components")]
    [SerializeField] Seeker seeker;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected Collider2D pfCollider;


    [Header("Scripts")]
    [Header("Custom Behavior")]    
    public bool directionLookEnabled = true;

    private Path path;

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

    // -------- Local Variables ---------- |


    public Vector2 Direction 
    {
        get => _direction;
        set { _direction = value; }
    }

    public Vector2 Force
    {
        get => _force;
        set { _force = value; }
    }

    public Vector3 Position
    {
        get => _currentPos;
        set { _currentPos = value; }
    }

    // IsMoving function 
    public bool IsMovingX
    {
        get => _isMovingX;

        private set
        {
            if (Mathf.Abs(_movement.x) > Mathf.Epsilon)
            {
                value = true;
            }
            else
            {
                value = false;
            }

            // Set the boolean in the animator 
            //anim.SetBool(AnimationStrings.isMoving, value);
        }
    }

    // IsMoving function 
    public float IsMovingY
    {
        get => _isMovingY;
        
        private set
        {
            value = _movement.y;

            // Set the boolean in the animator 
            //anim.SetFloat(AnimationStrings.yVelocity, value);
        }
    }

    public bool IsGrounded
    {
        get => _isGrounded;

        private set
        {
            _isGrounded = value;
        }
    }

public bool JumpEnabled
    {
        get => _jumpEnabled;

        set
        {
            _jumpEnabled = value;
        }
    }


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
            // If not facing right
            if (_isFacingRight != value)
            {
                // Flip the local scale of the objecct to preserve collider positions
                transform.localScale *= new Vector2(-1, 1);
            }
            // Set the variable with the value passet in the set 
            _isFacingRight = value;
        }
    }



    #region Flow Control

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pfCollider = GetComponent<Collider2D>();
        cam = GameObject.FindWithTag("CinemachineCam");
    }


    private void Start()
    {
        // Keep on repeating the script to update the path
        InvokeRepeating("UpdatePath", 0f, _pathUpdateFrequency);
    }

    private void Update()
    {
    }


    private void FixedUpdate()
    {
        

        // If find the target and can follow, follow it through the path
        if (TargetInRange() && _followEnabled)
        {
            Hunt();
        }
    }

    private void LateUpdate()
    {
        // For per-frame comparisons
        _lastPosition = transform.position;
        _lastGrounded = _isGrounded;
        _lastVelocity = new Vector2 (Mathf.Abs(rb.velocity.x), Mathf.Abs(rb.velocity.y));
    }

    #endregion

    #region Pathfinding AI

    private void UpdatePath()
    {
        // If object to seek found update path 
        if (_followEnabled && TargetInRange() && seeker.IsDone())
        {
            _targetPos = _target.position;

            seeker.StartPath(rb.position, _targetPos, OnPathComplete);
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
        if (_currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        jumpCheckOffset = 1;

        // Check if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);

        // Check if pathfinder is on the ground
        _isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.1f);

        // Calculate direction 
        Vector2 direction = ((Vector2)path.vectorPath[_currentWaypoint] - rb.position).normalized;

        // Calculate the force
        Vector2 force = direction * _groundSpeed * Time.deltaTime;


        // Jump following the grid dimensions
        if (direction.y > _jumpNodeHeightRequirement)
        {
            _jumpEnabled = true;

            if (_jumpEnabled && _isGrounded)
            {
                Jump();
            }

        }

        // Use the force(tm) to move the runner
        rb.AddForce(force, ForceMode2D.Impulse);

        // After moving, get the location of the next waypoint in the path
        float distance = Vector2.Distance(rb.position, path.vectorPath[_currentWaypoint]);

        // If the distance lest than the 
        if (distance < _nextWaypointDistance)
        {
            // 
            _currentWaypoint++;
        }

        // Flip the sprite depending on the direction 
        if (directionLookEnabled)
        {
            if (_movement.x > 0.01f)
            {
                transform.localScale = new Vector2(1,0);
            }
            else if (_movement.x < -0.01f)
            {
                transform.localScale = new Vector2(-1,0);
            }
        }
    }
    // Return true if target is in distance to be followed 
    private bool TargetInRange()
    {
        return Vector2.Distance(transform.position, _targetPos) < _aggroRange;
    }

    private void OnPathComplete(Path p)
    {
        // If no errors
        if (!p.error)
        {
            path = p;
            _currentWaypoint = 0;
        }

    }

    public void Jump()
    {
        if (_movement.x > 0.01f)
        {
            SetFacing();
            rb.AddForce(new Vector2(0,7) * _groundSpeed * jumpModifier, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);

        }
        else if (_movement.x < -0.01f)
        {
            // Flip sprite left if rb is moving left
            SetFacing();
            rb.AddForce(new Vector2(0,7) * _groundSpeed * jumpModifier, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
    }

    #endregion


    private void SetFacing()
    {
        // If the pathfinder is moving right but is not facing to the right
        if (_movement.x > Mathf.Epsilon && !IsFacingRight)
        {
            // Face Right
            IsFacingRight = true;
        }
        // If the pathfinder is moving left but is not facing to the left 
        else if (_movement.x < Mathf.Epsilon && IsFacingRight)
        {
            // Face Left
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

    private bool IsMoving()
    {
       float mvmt = _movement.x;

        if (Mathf.Abs(mvmt) > Mathf.Epsilon )
        {
            _isMovingX = true;
        }

        return IsMovingX;
    }

    private IEnumerator MovementValues()
    {
        yield return new WaitForSeconds(0.01f);

        _movement = rb.velocity;
        IsMoving();
        SetFacing();

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the 
        if (collision.tag == "Dervy")
        {
            if (JumpEnabled && IsGrounded == true)
            {
                // Make the pathfinder jump
                Jump();
                Debug.Log("PathfinderJumpImpulse made the Pathfinder jump!");
            }
        }
    }
}