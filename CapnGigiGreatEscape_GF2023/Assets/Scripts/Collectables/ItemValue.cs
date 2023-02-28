using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemValue : MonoBehaviour
{
    private Animator animatorD;
    public int diamondValue = 20;
    public SoundEffect diamondAudio;
    
    void Start()
    {
                animatorD = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if(player){
           //diamond value is collected
            player.Diamonds += diamondValue;
            animatorD.SetTrigger("Collect");
            diamondAudio.PlaySoundEffect();
            // Destroy the collectable
            Destroy(gameObject, 0.5f);
        }
    }

}
