using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public float playerCoins;

    public void Start()
    {
        playerCoins = 0f;
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        GameObject collectable = trigger.gameObject;

        if (collectable.tag == "Collectables")
        {
            Debug.Log("Collectable trigger: " + collectable);

            if (collectable.name == "Gold Coin")
            {
                Debug.Log("collectable.name: " + collectable);

                playerCoins += 1;
            }
            // do something
            // increace score based off the collectable
            // give bost based off collectable

            Destroy(collectable);

        }
    }
}
