using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompAttack : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake(){
        rb = GetComponentInParent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision){
        // Get script from the target
        Damageable damageable = collision.GetComponentInParent<Damageable>();
        // Check if can be hit 
        if(damageable != null){
            // Destroy just the enemy head hit box because the hitbox was destroyed too late and the player was able to jump a second time but on an invisible object
            Destroy(collision.gameObject);
            // Kill the enemy
            damageable.Health = 0;
            // Create Jump inpulse to jump on enemy head
            rb.velocity = new Vector2(rb.velocity.x, 7);
        }
    }
}
