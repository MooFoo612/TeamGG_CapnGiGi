using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ListFactory))]
public class PowerupFactory : ListFactory
{
    private Vector3 spawnLocation;
    private Transform powerupParent; 

    private void Awake()
    {
        // Picks its place in the hierarchy 
        powerupParent = GameObject.Find("Power-ups_Active").transform;

        // Gets the objects spawn location
        spawnLocation = transform.position;
    }
    private void Start()
    {
        GenerateRandomPowerup(powerups, spawnLocation, powerupParent);
    }

    private void GenerateRandomPowerup(List<GameObject> powerupList, Vector3 spawnLocation, Transform powerupParent)
    {

        int randomPowerup = UnityEngine.Random.Range(0, powerupList.Count - 1);

        
        int randomRoll = Random.Range(0, 100);


        if (randomRoll <= 75)
        {
            Debug.Log("Better luck next time! Your Roll: " + randomRoll);
            return;
        }
        else
        {
            Instantiate(powerupList[randomPowerup], spawnLocation, Quaternion.identity, powerupParent);
        }
    }
}
