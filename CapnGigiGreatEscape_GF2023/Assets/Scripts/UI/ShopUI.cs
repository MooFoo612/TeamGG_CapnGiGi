using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header ("Shop Events")]
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] Button openShopButton;
    [SerializeField] Button closeShopButton;

    public int[,] shopItems = new int[7, 7];
    public float coins;
    public float diamond;
    public TMP_Text coinsText;
    public TMP_Text diamondsText;



    void Start(){
        // Open and close the shop
        AddShopEvents();
        // Update the cost of the items
        coinsText.text = "X " + coins.ToString();
        // ID's of the item
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;
        shopItems[1, 6] = 6;
        // Prices in coins of the items
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 40;
        shopItems[2, 5] = 50;
        shopItems[2, 6] = 60;
        // Prices in diamonds of the items
        shopItems[3, 1] = 10;
        shopItems[3, 2] = 20;
        shopItems[3, 3] = 30;
        shopItems[3, 4] = 40;
        shopItems[3, 5] = 50;
        shopItems[3, 6] = 60;
        // Quantity of items
        shopItems[4, 1] = 1;
        shopItems[4, 2] = 1;
        shopItems[4, 3] = 1;
        shopItems[4, 4] = 1;
        shopItems[4, 5] = 1;
        shopItems[4, 6] = 1;

    }
    
    public void Buy(){
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        // Check if have enought coins
        if(coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID]){
            // Update the player coins
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID];
            // Increase quantity
            // shopItems[4, ButtonRef.GetComponent<ButtonInfo>().itemID]++;
            // Update coins text 
            coinsText.text = "X " + coins.ToString();
            // Update quantity text 
            //ButtonRef.GetComponent<ButtonInfo>().quantityText.text = shopItems[4, ButtonRef.GetComponent<ButtonInfo>().itemID].ToString();
        }
    }


    void AddShopEvents(){
        // Open the shop
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);
        // Close the shop UI
        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);
    }

    void OpenShop(){
        menuUI.SetActive(false);
        shopUI.SetActive(true);
    }

    void CloseShop(){
        shopUI.SetActive(false);
        menuUI.SetActive(true);
    }
}
