using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSwordsPickup : MonoBehaviour
{
    public int swordsAmount = 50;

    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if(player){
            // Add health to the character 
            player.ThrowingSwords += swordsAmount;
            // Destroy the collectable
            Destroy(gameObject);
        }
    }
}
