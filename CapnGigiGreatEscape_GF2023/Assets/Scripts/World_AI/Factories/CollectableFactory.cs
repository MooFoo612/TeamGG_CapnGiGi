using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : Factory
{
    private Vector3 spawnLocation;
    private Transform collectableParent;

    private void Awake()
    {
        collectableParent = GameObject.Find("Collectables_Active").transform;
        spawnLocation = transform.position;
    }
    private void Start()
    {
        GenerateRandomCollectable(collectables, spawnLocation, collectableParent);
        
    }
    private void GenerateRandomCollectable(List<GameObject> collectablePrefabs, Vector3 spawnLocation, Transform collectableParent)
    {
        int randomCollectable = UnityEngine.Random.Range(0, collectablePrefabs.Count);
        //Debug.Log("collectables spawnable" + (collectablePrefabs.Count - 1));
        Instantiate(collectablePrefabs[randomCollectable], spawnLocation, Quaternion.identity, collectableParent);
    }
}
