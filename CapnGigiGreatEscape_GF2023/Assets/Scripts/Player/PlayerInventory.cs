using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        GameObject collectable = trigger.gameObject;

        if (collectable.tag == "Collectables")
        {
            Debug.Log("Collectable trigger: " + collectable);

            // do something
            // increace score based off the collectable
            // give bost based off collectable

            Destroy(collectable);

        }
    }
}
