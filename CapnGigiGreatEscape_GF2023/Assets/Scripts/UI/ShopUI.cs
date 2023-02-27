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
    public float diamonds;
    public TMP_Text coinsText;
    public TMP_Text diamondsText;
    public PlayerInventory playerInv;
    public Button item1button;
    public Button item2button;
    public Button item3button;
    public Button item4button;


    private void Awake(){
        playerInv = GetComponent<PlayerInventory>();
    }
    
    private void OnEnable() {
        // Update the coins and diamond text
        coinsText.text = "X " + PlayerPrefs.GetInt("coins").ToString();
        diamondsText.text = "X " + PlayerPrefs.GetInt("diamonds").ToString();

        // Open and close the shop
        //AddShopEvents();
        // Update the coins and diamond text
        coinsText.text = "X " + PlayerPrefs.GetInt("coins").ToString();
        diamondsText.text = "X " + PlayerPrefs.GetInt("diamonds").ToString();
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
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 10;
        shopItems[3, 3] = 20;
        shopItems[3, 4] = 0;
        shopItems[3, 5] = 0;
        shopItems[3, 6] = 0;
        // Quantity of items
        shopItems[4, 1] = 0;
        shopItems[4, 2] = 0;
        shopItems[4, 3] = 0;
        shopItems[4, 4] = 1; 
        shopItems[4, 5] = 0;
        shopItems[4, 6] = 0;
    }

    public void Update(){
        if(PlayerPrefs.GetInt("purchasedDoubleJump") == 1){
            item1button.interactable = false;
        }
        if(PlayerPrefs.GetInt("purchasedDash") == 1){
            item2button.interactable = false;
        }  
        if(PlayerPrefs.GetInt("purchasedAirDash") == 1){
            item3button.interactable = false;
        }           
    }
    public void Buy(){
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        // Check if have enought coins and diamonds
        if(PlayerPrefs.GetInt("coins") >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID] && PlayerPrefs.GetInt("diamonds") >= shopItems[3, ButtonRef.GetComponent<ButtonInfo>().itemID]){
            // Update the player coins and diamonds
            PlayerPrefs.SetInt("coins", (PlayerPrefs.GetInt("coins") - shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID]));
            PlayerPrefs.SetInt("diamonds", (PlayerPrefs.GetInt("diamonds") - shopItems[3, ButtonRef.GetComponent<ButtonInfo>().itemID]));
            // Increase quantity 
            if (shopItems[4, ButtonRef.GetComponent<ButtonInfo>().itemID] > 0){
                //shopItems[4, ButtonRef.GetComponent<ButtonInfo>().itemID]++;
                PlayerPrefs.SetInt("swords", (PlayerPrefs.GetInt("swords") + shopItems[4, ButtonRef.GetComponent<ButtonInfo>().itemID]));
            }
            // If is buying Double jump
            if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID] == 1){
                // Make the player double jump for ever
                PlayerPrefs.SetInt("purchasedDoubleJump", 1);
            }
            if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID] == 2){
                PlayerPrefs.SetInt("purchasedDash", 1);
            }
            if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().itemID] == 3){
                PlayerPrefs.SetInt("purchasedAirDash", 1);
            }
            
            // Update coins and diamond text 
            coinsText.text = "X " + PlayerPrefs.GetInt("coins").ToString(); //coinsText.text = "X " + coins.ToString();
            diamondsText.text = "X " + PlayerPrefs.GetInt("diamonds").ToString();
            // Update quantity text 
            //ButtonRef.GetComponent<ButtonInfo>().quantityText.text = shopItems[4, ButtonRef.GetComponent<ButtonInfo>().itemID].ToString();
        }
    }


    //void AddShopEvents(){
        // Open the shop
        //openShopButton.onClick.RemoveAllListeners();
        //openShopButton.onClick.AddListener(OpenShop);
        //// Close the shop UI
        //closeShopButton.onClick.RemoveAllListeners();
        //closeShopButton.onClick.AddListener(CloseShop);
    //}

    //void OpenShop(){
    //    menuUI.SetActive(false);
    //    shopUI.SetActive(true);
    //}

    public void CloseShop(){
        shopUI.SetActive(false);
        menuUI.SetActive(true);
    }
    
}
