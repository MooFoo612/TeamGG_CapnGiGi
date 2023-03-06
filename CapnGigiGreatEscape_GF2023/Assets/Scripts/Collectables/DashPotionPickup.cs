using System;
using System.Collections.Generic;
using UnityEngine;

public class DashPotionPickup : PowerupBank
{
    // List for removal check
    Animator anim;
    public AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for script in the collision object 
        PlayerInventory playerCollection = collision.GetComponent<PlayerInventory>();

        // If the collision is with the player
        if (playerCollection)
        {
            // Enable Dash
            playerCollection._temporaryDash = true;

            // Play animation
            anim.SetTrigger("collected");

            if (audioSource != null)
            {
                audioSource.Play();
            }

            // "Collect" the Powerup
            Destroy(gameObject, 0.1f);
        }      
        else
        {
            Debug.Log("Not the player!");
        }
    }
}
