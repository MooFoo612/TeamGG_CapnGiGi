using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashPotionPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if(player){
            player.TemporaryAirDash = true;
            Destroy(gameObject);
        }
    }
}
