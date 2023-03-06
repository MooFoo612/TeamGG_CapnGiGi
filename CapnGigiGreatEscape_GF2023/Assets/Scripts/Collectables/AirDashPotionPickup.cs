using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashPotionPickup : PowerupBank
{
    public AudioSource audioSource;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the script from the collision object 
        PlayerInventory playerCollection = collision.GetComponent<PlayerInventory>();

        if(playerCollection)
        {   
            playerCollection._temporaryAirDash = true;
            
            // Play animation
            anim.SetTrigger("collected");

            if (audioSource != null)
            {
                audioSource.Play();
            }

            Destroy(gameObject, 0.1f);
        }
        else
        {
            Debug.Log("Not the player!");
        }
    }
}
