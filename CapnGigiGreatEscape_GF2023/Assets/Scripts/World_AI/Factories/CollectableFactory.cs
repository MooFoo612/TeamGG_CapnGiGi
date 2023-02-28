using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : ListFactory
{
    private ListFactory lf;
    private List<GameObject> collectableList;
    private Vector3 spawnLocation;

    private void Awake()
    {
        spawnLocation = transform.position;
        lf = gameObject.AddComponent<ListFactory>(); 
        collectableList = lf.GenerateCollectableList();
    }
    private void Start()
    {
        GenerateRandomCollectable(collectableList, spawnLocation);
    }
    private void GenerateRandomCollectable(List<GameObject> collectableList, Vector3 spawnLocation)
    {
        int randomCollectable = UnityEngine.Random.Range(0, collectableList.Count - 1);
        Instantiate(collectableList[randomCollectable], spawnLocation, Quaternion.identity);
    }
}
