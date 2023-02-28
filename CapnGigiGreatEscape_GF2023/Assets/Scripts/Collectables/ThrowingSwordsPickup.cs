using UnityEngine;

public class ThrowingSwordsPickup : ListFactory
{
    // Ammunition Store
    public int swordsAmount = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for script in the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();

        // If collision is with the player
        if(player)
        {
            // Add ammunition to the player 
            player.ThrowingSwords += swordsAmount;

            // "Collect" the ammunition
            Destroy(gameObject, 0.25f);
        }
    }
}
