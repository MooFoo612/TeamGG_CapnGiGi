using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    void OnTriggerEnter2D(Collider2D collision){
        // Get script from the target
        Damageable damageable = collision.GetComponent<Damageable>();
        // Check if can be hit 
        if(damageable != null){
            // Reverse the knockback vector direction depending on the localScale of the parent (because this script is attached to an hitbox) 
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            // Hit the target
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
            // Testing: if hit successfully debug the hit 
            if(gotHit){
                Debug.Log(collision.name + " hit for " + attackDamage);
            }
        }
    }

}
