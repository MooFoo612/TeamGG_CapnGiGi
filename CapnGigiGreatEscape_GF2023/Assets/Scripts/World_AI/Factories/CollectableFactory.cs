using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : ListFactory
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
        int randomCollectable = UnityEngine.Random.Range(0, collectablePrefabs.Count - 1);
        Instantiate(collectablePrefabs[randomCollectable], spawnLocation, Quaternion.identity, collectableParent);
    }
}
