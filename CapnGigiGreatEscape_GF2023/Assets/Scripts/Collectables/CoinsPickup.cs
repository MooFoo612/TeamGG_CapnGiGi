using UnityEngine;

public class CoinsPickup : Factory
{
    // Coin Value
    public int coinsAmount = 20;

    // Animator
    private Animator animatorCoin;

    // Inspector Audio access
    public SoundEffect coinAudio;

    void Start()
    {
        // Get component Animator
        animatorCoin = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for script in the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();

        // If collision is with the player
        if (player)
        {
            // Add coins to the player 
            player.Coins += coinsAmount;

            // Animation and Audio
            animatorCoin.SetTrigger("Collect");
            coinAudio.PlaySoundEffect();

            // "Collect" the coin
            Destroy(gameObject, 0.25f);
        }
    }
}