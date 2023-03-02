using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPotionPickup : Factory
{
    private static List<GameObject> activeList;
    private GameObject doubleJumpPotion;

    Animator anim;
    private void Awake()
    {
        doubleJumpPotion = GameObject.Find("DoubleJumpPotion");
        anim = GetComponent<Animator>();
        activeList = powerups;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if(player){
            player.TemporaryDoubleJump = true;

            for (int powerup = 0; powerup < activeList.Count; powerup++)
            {
                if (activeList[powerup] == doubleJumpPotion)
                {
                    powerups.RemoveAt(powerup);
                    break;
                } 
            }
            // Play animation
            anim.SetTrigger("collected");
            Destroy(gameObject, 0.25f);
        }
    }
}
