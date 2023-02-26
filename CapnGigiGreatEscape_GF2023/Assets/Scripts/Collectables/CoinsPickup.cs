using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPickup : MonoBehaviour
{
    public int coinsAmount = 20;
    private Animator animatorCoin;


        void Start()
    {

                animatorCoin = gameObject.GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if(player){
            // Add health to the character 
            player.Coins += coinsAmount;
             animatorCoin.SetTrigger("Collect");
            // Destroy the collectable
            Destroy(gameObject, 0.5f);
        }
    }
}