using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    public int damage = 10;
    Rigidbody2D rb;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Give horizontal speed to the projectile (to add gravity to the shot just make the rb dynamic)
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        // get the script from the collision gameObject
        Damageable damageable = collision.GetComponent<Damageable>();
        // Check if can be hit 
        if(damageable != null){
            // Reverse the knockback vector direction depending on localScale 
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            // Hit the target
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            // Testing: if hit successfully debug the hit 
            if(gotHit){
                Debug.Log(collision.name + " hit for " + damage);
                // Destroy the projectile
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
