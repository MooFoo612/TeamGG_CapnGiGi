using System.Collections.Generic;
using UnityEngine;

public class DashPotionPickup : ListFactory
{
    // List for removal check
    private static List<GameObject> activeList;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        // Populate active list from ListFactory
        activeList = powerups;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for script in the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();

        // If the collision is with the player
        if (player)
        {
            // Enable Dash
            player.TemporaryDash = true;

            // Check the Powerup List
            for (int powerup = 0; powerup < activeList.Count - 1; powerup++)
            {
                // If there is a powerup in the active list named DashPotion
                if (activeList[powerup].name == "DashPotion")
                {
                    // Remove the item from the global list in ListFactory
                    powerups.RemoveAt(powerup);
                }
            }
            // Play animation
            anim.SetTrigger("collected");
            // "Collect" the Powerup
            Destroy(gameObject, 0.25f);
        }
    }
}
