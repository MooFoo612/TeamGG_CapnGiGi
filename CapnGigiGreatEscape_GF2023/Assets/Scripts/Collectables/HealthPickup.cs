using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 50;

    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision object 
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable){
            // Add health to the character 
            damageable.Heal(healthRestore);
            // Destroy the collectable
            Destroy(gameObject);
        }
    }

    
}
