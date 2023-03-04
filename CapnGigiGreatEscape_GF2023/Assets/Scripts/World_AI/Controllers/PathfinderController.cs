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
    private Path path;
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
    [SerializeField] protected bool jumpEnabled = true;
    [SerializeField] bool jumpBuffer;
    [SerializeField] float jumpTimer = 1f;
    [SerializeField] float jumpModifier = 0.5f;
    [SerializeField] float jumpCheckOffset = 0.1f;

    [Header("Collision Detection")]
    [SerializeField] RaycastHit2D check_N;
    [SerializeField] RaycastHit2D check_NE;
    [SerializeField] RaycastHit2D check_E;
    [SerializeField] RaycastHit2D check_SE;
    [SerializeField] RaycastHit2D check_S;
    [SerializeField] RaycastHit2D check_SW;
    [SerializeField] RaycastHit2D check_W;
    [SerializeField] RaycastHit2D check_NW;

    [Header("Components")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected Collider2D pfCollider;

    [Header("Scripts")]
    [SerializeField] Seeker _navigator;

    [Header("Custom Behavior")]
    public bool directionLookEnabled = true;

    private GameObject cam;
    private bool offCamera;

    // -------- Local Variables ---------- |


    public Vector2 Direction 
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public Vector2 Force
    {
        get { return _force; }
        set { _force = value; }
    }

    public Vector3 Position
    {
        get { return _currentPos; }
        set { _currentPos = value; }
    }

    // IsMoving function 
    public bool IsMovingX
    {
        get { return _isMovingX; }

        private set
        {
            // Set the value
            value = IsMoving(_movement.x);

            // Set the boolean in the animator 
            anim.SetBool(AnimationStrings.isMoving, value);
        }
    }

    // IsMoving function 
    public float IsMovingY
    {
        get { return _isMovingY; }
        
        private set
        {
            value = _movement.y;

            // Set the boolean in the animator 
            anim.SetFloat(AnimationStrings.yVelocity, value);
        }
    }

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
        _navigator = GetComponent<Seeker>();
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
        if (_followEnabled && TargetInRange() && _navigator.IsDone())
        {
            _targetPos = _target.position;

            _navigator.StartPath(rb.position, _targetPos, OnPathComplete);
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
        






        // Check if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);









        // Check if pathfinder is on the ground
        _isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.1f);

        // Calculate direction 
        Vector2 direction = ((Vector2)path.vectorPath[_currentWaypoint] - rb.position).normalized;

        // Calculate the force
        Vector2 force = direction * _groundSpeed * Time.deltaTime;


        // Jump following the grid dimensions
        if (direction.y > _jumpNodeHeightRequirement && _targetPos.y > (Position.y + 1))
        {
            jumpEnabled = true;

            if (jumpEnabled && _isGrounded)
            {
                Jump();
            }

        }

        // Use the force(tm) to move the runner
        rb.AddForce(force);

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
            if (rb.velocity.x > 0.01f)
            {
                transform.localScale = new Vector2(1,0);
            }
            else if (rb.velocity.x < -0.01f)
            {
                transform.localScale = new Vector2(-1,0);
            }
        }
    }
    // Return true if target is in distance to be followed 
    private bool TargetInRange()
    {
        return Vector2.Distance(transform.position, _target.transform.position) < _aggroRange;
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
            rb.AddForce(new Vector2(3, 3) * _groundSpeed * jumpModifier);
            anim.SetBool("isJumping", true);

        }
        else if (_movement.x < -0.01f)
        {
            // Flip sprite left if rb is moving left
            SetFacing();
            rb.AddForce(new Vector2(3, 3) * _groundSpeed * jumpModifier);
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

    private bool IsMoving(float movement)
    {

        if (Mathf.Abs(movement) > Mathf.Epsilon )
        {
            _isMovingX = true;
        }

        return IsMovingX;
    }

    private IEnumerator MovementValues()
    {
        yield return new WaitForSeconds(0.01f);

        _movement = rb.velocity;
        IsMoving(_movement.x);
        SetFacing();

        
    }
}