using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Factory))]
public class CoinWarehouse : Factory
{
    // Spawn Location
    private Vector3 spawnLocation;

    //Hierarchy location
    private Transform coinParent;

    // To call coin from list
    private GameObject goldCoin;
    private GameObject player;
    private Vector3 playerPosition;
    private float distanceToDestroy;

    public int coinCountdown = 10;

    //private Vector3 cloneSpawnPosition;
    //private float distanceToPlayer;
    //public float distanceToDestroyForReverse = 100f; 
    //public float checkDistance = 40f;
    //private GameObject AIScripts;
    //private WorldGenerator worldGenerator;

    // To call diamond from list
    //public GameObject blueDiamond;

    
    
    private void Awake()
    {
        coinParent = GameObject.Find("Coins_Active").transform;
        
        spawnLocation = gameObject.transform.position;
        /*
        AIScripts = GameObject.FindGameObjectWithTag("LevGen");
        worldGenerator = AIScripts.GetComponent<WorldGenerator>();
        cloneSpawnPosition = transform.position;
        player = GameObject.Find("CapnGigi");
        */

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
