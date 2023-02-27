using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPotionPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if(player){
            player.TemporaryDoubleJump = true;
            Destroy(gameObject);
        }
    }
}
