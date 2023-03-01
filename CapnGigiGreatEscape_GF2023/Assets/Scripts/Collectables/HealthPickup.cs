using UnityEngine;

public class HealthPickup : Factory
{
    // Animator
    private Animator animatorHeart;

    // Heal Value
    public int healthRestore = 50;

    void Start()
    {
        // Get component animator
        animatorHeart = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for script in the collision object 
        Damageable damageable = collision.GetComponent<Damageable>();

        // If damageable script is present on the collision
        if (damageable)
        {
            // Add health to the player 
            damageable.Heal(healthRestore);

            // Animation and Audio 
            animatorHeart.SetTrigger("Collect");

            // "Collect" the health
            Destroy(gameObject, 0.25f);
        }
    }

    
}
