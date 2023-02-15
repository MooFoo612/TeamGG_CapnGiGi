using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySwords : MonoBehaviour
{
    public TMP_Text swordsAmountText;
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
        swordsAmountText.text = "x " + playerInv.ThrowingSwords;
    }

    private void OnEnable(){
        playerInv.swordsAmountChanged.AddListener(OnSwordsAmountChanged);
    }

    private void OnDisable(){
        playerInv.swordsAmountChanged.RemoveListener(OnSwordsAmountChanged);
    }
    

    private void OnSwordsAmountChanged(int amount){
        // Update the swords amount 
        swordsAmountText.text = "x " + amount;
    }
}
