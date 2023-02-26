using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Animator animatorHeart;
    public int healthRestore = 50;

    void Start()
    {
                animatorHeart = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision object 
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable){
            // Add health to the character 
            damageable.Heal(healthRestore);
            animatorHeart.SetTrigger("Collect");
            // Destroy the collectable
            Destroy(gameObject, 0.5f);
        }
    }

    
}
