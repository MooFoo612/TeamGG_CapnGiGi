using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPotionPickup : ListFactory
{
    private static List<GameObject> activeList;

    private void Awake()
    {
        activeList = powerups;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if(player){
            player.TemporaryDoubleJump = true;

            for (int powerup = 0; powerup < activeList.Count - 1; powerup++)
            {
                if (activeList[powerup].name == "DoubleJumpPotion")
                {
                    powerups.RemoveAt(powerup);
                }

            }
            Destroy(gameObject, 0.25f);
        }
    }
}
