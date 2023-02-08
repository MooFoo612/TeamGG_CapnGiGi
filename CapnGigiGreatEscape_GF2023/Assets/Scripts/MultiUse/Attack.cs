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
            // Hit the target
            bool gotHit = damageable.Hit(attackDamage, knockback);
            // Testing: if hit successfully debug the hit 
            if(gotHit){
                Debug.Log(collision.name + " hit for " + attackDamage);
            }
            
        }
    }

}
