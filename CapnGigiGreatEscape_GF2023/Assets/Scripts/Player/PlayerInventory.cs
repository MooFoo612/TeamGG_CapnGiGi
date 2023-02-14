using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{   
    public UnityEvent<int> swordsAmountChanged;

    [SerializeField] private int _throwingSwords = 10;
    public int ThrowingSwords{
        get {
            return _throwingSwords;
        } set {
            _throwingSwords = value;
            // Make the value don't go below 0 for GUI stuff
            if(_throwingSwords <= 0){
                _throwingSwords = 0;
            }
            // Update unity event for the sword GUI
            swordsAmountChanged?.Invoke(_throwingSwords);
        }
    }











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
