using UnityEngine;

public class ThrowingSwordsPickup : Factory
{
    // Ammunition Store
    public int swordsAmount = 20;
    Animator anim;
    private void Start(){
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for script in the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();

        // If collision is with the player
        if(player)
        {
            // Add ammunition to the player 
            player.ThrowingSwords += swordsAmount;
            // Play animation
            anim.SetTrigger("collected");
            // "Collect" the ammunition
            Destroy(gameObject, 0.25f);
        }
    }
}
