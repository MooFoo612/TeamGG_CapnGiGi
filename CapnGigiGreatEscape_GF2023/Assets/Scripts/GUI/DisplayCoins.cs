using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCoins : MonoBehaviour
{
    public TMP_Text coinsAmountText;
    PlayerInventory playerInv;

    private void Awake(){
        // Find the player and get the inventory script component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerInv = player.GetComponent<PlayerInventory>();
        // Testing
        if(playerInv == null){
            Debug.Log("No Player found in the scene");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Update the swords amount 
        coinsAmountText.text = "x " + playerInv.Coins;
    }

    private void OnEnable(){
        playerInv.coinsAmountChanged.AddListener(OnCoinsAmountChanged);
        
    }

    private void OnDisable(){
        playerInv.coinsAmountChanged.RemoveListener(OnCoinsAmountChanged);
    }
    

    private void OnCoinsAmountChanged(int amount){
        // Update the swords amount 
        coinsAmountText.text = "x " + amount;
    }
}
