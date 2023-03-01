using System.Collections.Generic;
using UnityEngine;

public class CoinFactory : ListFactory
{
    // Spawn Location
    private Vector3 spawnLocation;

    //Hierarchy location
    private Transform coinParent;

    // To call coin from list
    private GameObject goldCoin;

    // To call diamond from list
    private GameObject blueDiamond;

    private int coinCountdown = 60;

    private void Awake()
    {
        coinParent = GameObject.Find("Coins_Active").transform;

        // Get position of spawn point
        spawnLocation = gameObject.transform.position;

    }
    private void Start()
    {
        // When object is spawned generate a random coin from the list
        GenerateCoins(coins, spawnLocation, coinParent);
    }
    private void GenerateCoins(List<GameObject> coins, Vector3 spawnLocation, Transform coinParent)
    {
        // If the coin countdown has reached 0
        if (coinCountdown != 0)
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

            // Spawn the blue diamond
            Instantiate(goldCoin, spawnLocation, Quaternion.identity, coinParent);

            // Reset the counter
            coinCountdown--;
        }
        else 
        {
            // Check coins list for the Blue Diamond
            foreach (GameObject coin in coins)
            {
                if (coin.name == "BlueDiamond")
                {
                    blueDiamond = coin;
                    break;
                }
            }

            // Spawn a random coin at the spawn point
            Instantiate(blueDiamond, spawnLocation, Quaternion.identity, coinParent);

            // Reset the counter
            coinCountdown = 60;
        }
    }
}
