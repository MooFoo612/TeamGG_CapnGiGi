using UnityEngine;

public class diamondPickup : Factory
{
    private Animator animatorD;
    public int diamondValue = 20;
    public SoundEffect diamondAudio;
    
    void Start()
    {
        // Get component Animator
        animatorD = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();

        // If collision is with the player
        if (player)
        {
           // Add diamond value to player
            player.Diamonds += diamondValue;

            // Animation and Audio
            animatorD.SetTrigger("Collect");
            diamondAudio.PlaySoundEffect();

            // "Collect" the diamond
            Destroy(gameObject, 0.25f);
        }
    }
}
