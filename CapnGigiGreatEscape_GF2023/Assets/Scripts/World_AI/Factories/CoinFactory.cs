using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Factory))]
public class CoinFactory : Factory
{
    // Spawn Location
    private Vector3 spawnLocation;

    //Hierarchy location
    private Transform coinParent;

    // To call coin from list
    private GameObject goldCoin;

    public int coinCountdown = 10;
    // To call diamond from list
    //public GameObject blueDiamond;

    
    
    private void Awake()
    {
        coinParent = GameObject.Find("Coins_Active").transform;

        // Get position of spawn point
        spawnLocation = gameObject.transform.position;

    }
    public void Start()
    {
        
        // When object is spawned generate a random coin from the list
        GenerateCoins(coins, spawnLocation, coinParent);
    }
    private void GenerateCoins(List<GameObject> coins, Vector3 spawnLocation, Transform coinParent)
    {
        //Debug.Log("coinCountdown: " + PlayerPrefs.GetInt("coinsCountdown"));
        // If the coin countdown has reached 0
        if (PlayerPrefs.GetInt("coinsCountdown") != 0) 
        {
            // Check coins list for the Blue Diamond
            foreach (GameObject coin in coins)
            {
                if (coin.name == "GoldCoin")
                {
                    goldCoin = coin;
                    break;
                }
            }
            //Spawn the blue diamond
            Instantiate(goldCoin, spawnLocation, Quaternion.identity, coinParent);

            // Update the counter
            PlayerPrefs.SetInt("coinsCountdown", PlayerPrefs.GetInt("coinsCountdown") -1);

        // If timer is finished 
        } else {//if(PlayerPrefs.GetInt("coinCountdown") == 0) {

            foreach (GameObject diamond in coins)
            {
                if (diamond.name == "BlueDiamond")
                {
                    blueDiamond = diamond;
                }
            }
            //Spawn a random coin at the spawn point
            Instantiate(blueDiamond, spawnLocation, Quaternion.identity, coinParent);

            // Reset the counter
            PlayerPrefs.SetInt("coinsCountdown",  10);
            // Check coins list for the Blue Diamond
            

            
        }
    }
}
