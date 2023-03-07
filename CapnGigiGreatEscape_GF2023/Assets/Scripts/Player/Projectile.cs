using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(6f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    public int damage = 10;
    Rigidbody2D rb;
    Collider2D touchingCol;
    public ContactFilter2D castFilter;
    // If sword is going  right set the vector 2 to the right direction, otherwise set it to the left direction
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    public float wallDistance = 0.2f;
    [SerializeField] private bool _isOnWall;

    // IsOnWall function
    public bool IsOnWall {
        get{
            // Return the value inside the _isOnWall variable just created
            return _isOnWall;
        } private set {
            // Set _isOnWall to the value is gonna be passed into the set
            _isOnWall = value;
        }
    }
    
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {       
        // Give horizontal speed to the projectile (to add gravity to the shot just make the rb dynamic)
        rb.velocity = new Vector3 (moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void FixedUpdate()
    {
        //if (gameObject.tag == "ThrowingSword")
        //{
        //    gameObject.transform.RotateAround(transform.position, Vector2.left, 450 * Time.fixedDeltaTime);
        //}

        if (PlayerPrefs.GetInt("throwSwordAttackPowerUp") == 1){
            rb.bodyType = RigidbodyType2D.Kinematic;
            damage += 20;
            moveSpeed = new Vector2(10f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision gameObject
        Damageable damageable = collision.GetComponent<Damageable>();
        // Check if can be hit 
        if(damageable != null){
            // Reverse the knockback vector direction depending on localScale 
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            // Hit the target
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            // Testing: if hit successfully debug the hit 
            if(gotHit){
                //Debug.Log(collision.name + " hit for " + damage);
                // Destroy the projectile
                Destroy(gameObject);
            }
        }
    }
    
    void Update(){
        // Check if collide with wall
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        // If is colliding
        if(IsOnWall){
            // Destroy the projectile
            Destroy(gameObject); 
        }
    }
}
