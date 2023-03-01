using System.Collections.Generic;
using UnityEngine;

public class TreasureFactory : Factory
{
    private Vector3 spawnLocation;
    private Transform treasureParent;

    private void Awake()
    {
        treasureParent = GameObject.Find("Treasures_Active").transform;
        spawnLocation = transform.position;
    }
    private void Start()
    {
        GenerateRandomTreasure(treasures, spawnLocation, treasureParent);
    }
    private void GenerateRandomTreasure(List<GameObject> treasureList, Vector3 spawnLocation, Transform treasureParent)
    {
        int randomTreasure = UnityEngine.Random.Range(0, treasureList.Count - 1);
        Instantiate(treasureList[randomTreasure], spawnLocation, Quaternion.identity, treasureParent);
    }
}
