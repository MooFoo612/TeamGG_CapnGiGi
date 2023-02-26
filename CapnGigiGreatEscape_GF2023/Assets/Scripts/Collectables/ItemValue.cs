using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemValue : MonoBehaviour
{
    private Animator animatorD;
    public float itemValue = 0f;

    void Start()
    {
                animatorD = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        // Get the script from the collision object 
        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if(player){
           //diamond value is collected
            animatorD.SetTrigger("Collect");
            // Destroy the collectable
            Destroy(gameObject, 0.5f);
        }
    }

}
