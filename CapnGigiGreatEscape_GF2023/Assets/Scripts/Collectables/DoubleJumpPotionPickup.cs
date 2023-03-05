using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPotionPickup : CollectableWarehouse
{
    [SerializeField] GameObject doubleJumpPotion;
    [SerializeField] AudioSource audioSource;
    List<GameObject> activeList;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < powerups.Count; i++)
        {
            if (powerups[i].name == "DoubleJumpPotion")
            {
                doubleJumpPotion = powerups[i];
                activeList.Add(doubleJumpPotion);
                break;
            }
        }
    }

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();

        if(player)
        {
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
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Destroy(gameObject, 0.1f);
        }
    }
}
