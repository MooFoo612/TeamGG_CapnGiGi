using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashPotionPickup : Factory
{
    private static List<GameObject> activeList;

    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        activeList = powerups;
    }
    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if(player){
            player.TemporaryAirDash = true;
            
            for (int powerup = 0; powerup < activeList.Count; powerup++)
            {
                if (activeList[powerup].name == "AirDashPotion") 
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
